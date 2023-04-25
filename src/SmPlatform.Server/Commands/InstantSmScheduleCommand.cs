using MassTransit.Mediator;
using SmPlatform.Domain.Events;

namespace SmPlatform.Server.Commands
{
    /// <summary>
    /// 即时短信调度命令
    /// </summary>
    public record InstantSmScheduleCommand : Request<string>
    {
        public SmsSendingScheduledEvent SmsSendingEvent { get; set; }
    }
}
