﻿using AutoMapper;
using MassTransit.Mediator;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;
using SmPlatform.Model.ViewModels;

namespace SmPlatform.ManagementApi.Application.Commands;

/// <summary>
/// 短信通道添加命令处理器
/// </summary>
public class ChannelAddHandler : MediatorRequestHandler<ChannelAddCommand, ApiResult<ChannelInformation>>
{
    private readonly IChannelRepository _channelRepository;

    private readonly IMapper _mapper;


    public ChannelAddHandler(IChannelRepository channelRepository, IMapper mapper)
    {
        _channelRepository = channelRepository;
        _mapper = mapper;
    }

    protected override async Task<ApiResult<ChannelInformation>> Handle(ChannelAddCommand request, CancellationToken cancellationToken)
    {
        var channel = _mapper.Map<Channel>(request);
        var rank = await _channelRepository.CountAsync(cancellationToken);

        channel.Level = (int)rank;

        // 这种情况不应该发生，因为短信通道数量绝不会超过 int 的范围
        if (channel.Level != rank)
            throw new InvalidOperationException("long -> int 的类型转换损失");

        // 验证密钥 ID 的唯一性
        if (await _channelRepository.ExistsAsync(c => c.Platform.AccessKeyId == request.Platform.AccessKeyId))
            return ApiResultFactory.Fail<ChannelInformation>("密钥 ID 已经存在");


        var result = _mapper.Map<ChannelInformation>(await _channelRepository.AddAsync(channel));

        await _channelRepository.UnitWork.SaveEntitiesAsync(cancellationToken);

        return ApiResultFactory.Success(result);
    }
}
