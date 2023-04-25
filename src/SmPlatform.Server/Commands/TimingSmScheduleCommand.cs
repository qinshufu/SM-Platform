using MassTransit.Mediator;
using SmPlatform.Domain.Events;

namespace SmPlatform.Server.Commands;

/// <summary>
/// 定时短信调度命令
/// </summary>
public record TimingSmScheduleCommand : Request<string>
{
    public SmsSendingScheduledEvent SmsSendingEvent { get; set; }
}
