using AutoMapper;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Events;
using SmPlatform.Server.Models;

namespace SmPlatform.Server
{
    /// <summary>
    /// 映射配置
    /// </summary>
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SmsSendingScheduledEvent, ShortMessage>();
            CreateMap<SmsSendingScheduledEvent, TimedMessage>().ForMember(m => m.ScheduledTime, o => o.MapFrom(e => e.Timing));
            CreateMap<TimedMessage, ShortMessage>();
        }
    }
}
