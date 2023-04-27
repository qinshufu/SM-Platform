using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Polly;
using SmPlatform.Server.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace SmPlatform.Server.Services;

/// <summary>
/// 短信队列
/// </summary>
public class SmSharedQueue : ISmSharedQueue
{
    private readonly IDatabase _db;

    private readonly ILogger<SmSharedQueue> _logger;

    const string QueueKey = "sending-message";

    const int QueueSize = 200;

    public SmSharedQueue(ConnectionMultiplexer multiplexer, ILogger<SmSharedQueue> logger)
    {
        _db = multiplexer.GetDatabase(0);
        _logger = logger;
    }

    public async Task<ShortMessage> DequeueAsync(CancellationToken cancellationToken = default)
    {
        var value = await _db.ListLeftPopAsync(QueueKey);

        if (value.IsNullOrEmpty)
            throw new InvalidOperationException("短信队列为空");

        var sm = JsonSerializer.Deserialize<ShortMessage>(value.ToString());

        return sm;
    }

    public async Task<List<ShortMessage>> DequeueRangeAsync(int number, CancellationToken cancellationToken = default)
    {
        var value = await _db.ListLeftPopAsync(QueueKey, number);

        if (value.IsNullOrEmpty())
            throw new InvalidOperationException("短信队列为空");

        var messages = JsonSerializer.Deserialize<List<ShortMessage>>(value.ToString());

        return messages;
    }

    public async Task EnqueueAsync(ShortMessage shortMessage, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("准备将消息推入队列中：" + shortMessage);

        var value = JsonSerializer.Serialize(shortMessage);

        var policy = Policy.HandleResult(false).WaitAndRetryAsync(5, i => TimeSpan.FromSeconds(Math.Pow(2, i) * 0.5));

        var retryNumber = 0;

        var result = await policy.ExecuteAndCaptureAsync(async () =>
        {
            if (retryNumber is not 0)
            {
                _logger.LogInformation($"将消息推入队列中，第{retryNumber}次重试");
            }

            var tran = _db.CreateTransaction();

            tran.AddCondition(Condition.ListLengthLessThan(QueueKey, QueueSize));
            _ = tran.ListRightPushAsync(QueueKey, value);

            return await tran.ExecuteAsync();
        });

        if (result.Result is false)
            throw new InvalidOperationException("短信队列已满");
    }

    public async Task EnqueueRangeAsync(IEnumerable<ShortMessage> shortMessages, CancellationToken cancellationToken = default)
    {
        shortMessages = shortMessages.ToArray();
        _logger.LogInformation("准备将多条消息推入队列中：" + shortMessages);

        var value = JsonSerializer.Serialize(shortMessages);

        var policy = Policy.HandleResult(false).WaitAndRetryAsync(5, i => TimeSpan.FromSeconds(Math.Pow(2, i) * 0.5));

        var retryNumber = 0;

        var result = await policy.ExecuteAndCaptureAsync(async () =>
        {
            if (retryNumber is not 0)
            {
                _logger.LogInformation($"将消息推入队列中，第{retryNumber}次重试");
            }

            var tran = _db.CreateTransaction();

            tran.AddCondition(Condition.ListLengthLessThan(QueueKey, QueueSize - shortMessages.Count()));
            _ = tran.ListRightPushAsync(QueueKey, value);

            return await tran.ExecuteAsync();
        });

        if (result.Result is false)
            throw new InvalidOperationException("短信队列已满");

    }
}
