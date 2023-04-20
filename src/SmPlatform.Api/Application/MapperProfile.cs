﻿using AutoMapper;
using SmPlatform.Api.Application.Commands;
using SmPlatform.BuildingBlock.Extensions;
using SmPlatform.Model.DataModels;
using SmPlatform.Model.ViewModels;

namespace SmPlatform.Api.Application;

/// <summary>
/// AutoMapper 配置
/// </summary>
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Channel, ChannelInformation>();
        CreateMap<ChannelAddCommand, Channel>();
        CreateMap<Channel, ChannelBasicInformation>();
        CreateMap<Pagination<Channel>, Pagination<ChannelBasicInformation>>();
    }
}