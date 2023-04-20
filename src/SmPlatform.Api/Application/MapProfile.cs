using AutoMapper;
using SmPlatform.Model.DataModels;
using SmPlatform.Model.ViewModels;

namespace SmPlatform.Api.Application;

/// <summary>
/// AutoMapper 配置
/// </summary>
public class MapProfile : Profile
{
    protected MapProfile()
    {
        CreateMap<Channel, ChannelInformation>();
    }
}
