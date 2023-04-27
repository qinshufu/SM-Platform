using Microsoft.Extensions.Logging;
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
        const int batchSize = 50;

        var messages = await _smQueue.DequeueRangeAsync(batchSize);

        var tasks = messages
            .Select(async m => (Message: m, Sended: await _smSender.SendAsync(m)))
            .Select(t => t.ContinueWith(t => t.Result.Sended switch
            {
                true => OnSendSuccessful(t.Result.Message),
                false => OnSendFailed(t.Result.Message)
            }, TaskContinuationOptions.OnlyOnRanToCompletion));

        await Task.WhenAll(tasks.ToArray());
    }

    private Task OnSendFailed(ShortMessage message)
    {
        _logger.LogInformation("消息发送成功: " + message);

        return Task.CompletedTask;
    }

    private Task OnSendSuccessful(ShortMessage message)
    {
        _logger.LogWarning("消息发送成功: " + message);

        // 当消息发送失败，入队重新发送
        return _smQueue.EnqueueAsync(message);
    }
}
