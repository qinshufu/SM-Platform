using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using Polly;
using SmPlatform.Domain.Events;
using SmPlatform.Instructure.Distribution;
using SmPlatform.Server.Commands;
using SmPlatform.Server.Services;

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
        if (_lockManager.Lock($"event:{nameof(SmsSendingScheduledEvent)}:" + context.MessageId.ToString()!, out @lock) is false)
        {
            _logger.LogInformation("无法获取短信调度事件的锁，该事件正在被其他实例处理\n" + context.Message);
            return Task.CompletedTask;
        }

        try
        {
            return context.Message.Timing switch
            {
                null => _mediator.Send(new InstantSmScheduleCommand { SmsSendingEvent = context.Message }),
                not null => _mediator.Send(new TimingSmScheduleCommand { SmsSendingEvent = context.Message })
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
