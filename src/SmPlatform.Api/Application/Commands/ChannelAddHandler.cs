using AutoMapper;
using MassTransit.Mediator;
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

    public ChannelAddHandler(IChannelRepository channelRepository, IMapper mapper)
    {
        _channelRepository = channelRepository;
        _mapper = mapper;
    }

    protected override async Task<ApiResult<ChannelInformation>> Handle(ChannelAddCommand request, CancellationToken cancellationToken)
    {
        var channel = _mapper.Map<Channel>(request);
        var result = _mapper.Map<ChannelInformation>(await _channelRepository.AddAsync(channel));

        return ApiResultFactory.Success(result);
    }
}
