using AutoMapper;
using MassTransit.Mediator;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;

namespace SmPlatform.Server.Commands
{
    /// <summary>
    /// 定时短信调度处理器
    /// </summary>
    public class TimingSmScheduleHandler : MediatorRequestHandler<TimingSmScheduleCommand>
    {
        private readonly ITimedSmRepository _timedSmRepository;

        private readonly IMapper _mapper;

        public TimingSmScheduleHandler(ITimedSmRepository timedSmRepository, IMapper mapper)
        {
            _timedSmRepository = timedSmRepository;
            _mapper = mapper;
        }

        protected override async Task Handle(TimingSmScheduleCommand request, CancellationToken cancellationToken)
        {
            // 该方法仅将定时短信保存到数据库中，实际定时短信的发送工作将由一个定时任务完成

            var timedSm = _mapper.Map<TimedMessage>(request.SmsSendingEvent);

            await _timedSmRepository.AddAsync(timedSm);
            await _timedSmRepository.UnitWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
