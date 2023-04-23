using MassTransit.Mediator;

namespace SmPlatform.Server.Commands
{
    /// <summary>
    /// 定时短信调度处理器
    /// </summary>
    public class TimingSmScheduleHandler : MediatorRequestHandler<TimingSmScheduleCommand>
    {
        protected override Task Handle(TimingSmScheduleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
