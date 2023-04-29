using Microsoft.Extensions.Logging;
using Polly;
using Quartz;
using SmPlatform.Server.Models;
using SmPlatform.Server.Services;

namespace SmPlatform.Server.Job;

/// <summary>
/// 短信发送任务
/// </summary>
public class SmSendJob : IJob
{
    private readonly ISmSharedQueue _smQueue;

    private readonly ISmSender _smSender;

    private readonly ILogger<SmSendJob> _logger;

    public SmSendJob(ISmSharedQueue smQueue, ISmSender smSender, ILogger<SmSendJob> logger)
    {
        _smQueue = smQueue;
        _smSender = smSender;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var endTime = DateTime.Now.AddMinutes(1);

        const int batchSize = 100;

        while (DateTime.Now < endTime)
        {
            var messages = default(List<ShortMessage>);

            try
            {
                messages = await _smQueue.DequeueRangeAsync(batchSize);
            }
            catch (InvalidOperationException)
            {
                // 从消息队列获取消息失败，消息队列为空
                return;
            }

            var _ = messages
                .AsParallel()
                .WithDegreeOfParallelism((int)Math.Floor(1.0 * messages.Count / Environment.ProcessorCount))
                .Select(m => (Sended: _smSender.SendAsync(m).Result, Message: m))
                .Select(r => r.Sended switch
                {
                    true => OnSendSuccessful(r.Message).Result,
                    false => OnSendFailed(r.Message).Result,
                })
                .ToArray();
        }
    }

    private Task<ShortMessage> OnSendFailed(ShortMessage message)
    {
        _logger.LogInformation("消息发送成功: " + message);

        return Task.FromResult(message);
    }

    private async Task<ShortMessage> OnSendSuccessful(ShortMessage message)
    {
        _logger.LogWarning("消息发送成功: " + message);

        var policy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(5, i => TimeSpan.FromSeconds(0.6 * Math.Pow(2, i)));

        var result = await policy.ExecuteAndCaptureAsync(() => _smQueue.EnqueueAsync(message));

        if (result.FinalException is not null) /// 运行失败
        {
            _logger.LogInformation("消息发送失败以后，重新入队失败，该消息将被遗失: " + message);
        }

        return message;
    }
}
