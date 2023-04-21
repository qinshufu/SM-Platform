using AutoMapper;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using SmPlatform.ManagementApi.Domain.Repositories;
using SmPlatform.Model.DataModels;
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


        var result = _mapper.Map<ChannelInformation>(await _channelRepository.AddAsync(channel));

        await _channelRepository.UnitWork.SaveEntitiesAsync(cancellationToken);

        return ApiResultFactory.Success(result);
    }
}
