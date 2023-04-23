using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using SmPlatform.Domain.Events;
using SmPlatform.Instructure.Distribution;
using SmPlatform.Server.Commands;

namespace SmPlatform.Server.Consumers;

/// <summary>
/// 短信发送调度事件消费者
/// </summary>
public class SmsSendingScheduledConsumer : IConsumer<SmsSendingScheduledEvent>
{
    private readonly IDistributedLockManager _lockManager;

    private readonly ILogger<SmsSendingScheduledConsumer> _logger;

    private readonly IMediator _mediator;

    public SmsSendingScheduledConsumer(
        IDistributedLockManager lockManager, ILogger<SmsSendingScheduledConsumer> logger, IMediator mediator)
    {
        _lockManager = lockManager;
        _logger = logger;
        _mediator = mediator;
    }

    public Task Consume(ConsumeContext<SmsSendingScheduledEvent> context)
    {
        var @lock = default(IDistributedLock);
        if (_lockManager.Lock(context.MessageId.ToString()!, out @lock) is false)
        {
            _logger.LogInformation("无法获取短信处理的锁，可能有其他的进程正在处理该短信\n" + context.Message);
            return Task.CompletedTask;
        }

        try
        {
            return context.Message switch
            {
                { Timing: null } => _mediator.Send(new InstantSmScheduleCommand { SmsSendingEvent = context.Message }),
                { Timing: not null } => _mediator.Send(new TimingSmScheduleCommand { SmsSendingEvent = context.Message })
            };
        }
        catch (Exception ex)
        {
            _logger.LogError("短信处理失败: " + context.Message + "\n" + ex);
        }
        finally
        {
            _ = @lock is not null ? _lockManager.Unlock(@lock) : false;
        }

        return Task.CompletedTask;
    }

}
