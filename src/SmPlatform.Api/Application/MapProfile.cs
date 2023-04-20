using AutoMapper;
using SmPlatform.Api.Application.Commands;
using SmPlatform.Model.DataModels;
using SmPlatform.Model.ViewModels;

namespace SmPlatform.Api.Application;

/// <summary>
/// AutoMapper 配置
/// </summary>
public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Channel, ChannelInformation>();
        CreateMap<ChannelAddCommand, Channel>();
    }
}
