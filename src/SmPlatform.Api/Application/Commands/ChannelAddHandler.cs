using AutoMapper;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using SmPlatform.Api.Domain;
using SmPlatform.Model.DataModels;
using SmPlatform.Model.ViewModels;

namespace SmPlatform.Api.Application.Commands;

/// <summary>
/// 短信通道添加命令处理器
/// </summary>
public class ChannelAddHandler : MediatorRequestHandler<ChannelAddCommand, ApiResult<ChannelInformation>>
{
    private readonly IChannelRepository _channelRepository;

    private readonly IMapper _mapper;

    private readonly IQueryable<Channel> _channels;

    public ChannelAddHandler(IChannelRepository channelRepository, IQueryable<Channel> channels, IMapper mapper)
    {
        _channelRepository = channelRepository;
        _mapper = mapper;
        _channels = channels;
    }

    protected override async Task<ApiResult<ChannelInformation>> Handle(ChannelAddCommand request, CancellationToken cancellationToken)
    {
        var channel = _mapper.Map<Channel>(request);
        var rank = await _channels.CountAsync();

        channel.Level = rank;

        var result = _mapper.Map<ChannelInformation>(await _channelRepository.AddAsync(channel));

        await _channelRepository.UnitWork.SaveEntitiesAsync(cancellationToken);

        return ApiResultFactory.Success(result);
    }
}
