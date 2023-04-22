using AutoMapper;
using SmPlatform.Domain.Events;
using SmPlatform.SmApi.Commads;

namespace SmPlatform.SmApi;

/// <summary>
/// mapper 配置
/// </summary>
public class MapperProfile : Profile
{
    protected MapperProfile()
    {
        CreateMap<SmSendCommand, SmsSendingScheduledEvent>();
    }
}
