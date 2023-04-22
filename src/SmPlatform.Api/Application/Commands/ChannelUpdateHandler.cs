using AutoMapper;
using MassTransit.Mediator;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;
using SmPlatform.ManagementApi.Application.Exceptions;
using SmPlatform.Model.ViewModels;
using System.Reflection;

namespace SmPlatform.ManagementApi.Application.Commands;

/// <summary>
/// 短信通道更新命令处理器
/// </summary>
public class ChannelUpdateHandler : MediatorRequestHandler<ChannelUpdateCommand, ApiResult<ChannelInformation>>
{
    private readonly IChannelRepository _channelRepository;

    private readonly IMapper _mapper;

    public ChannelUpdateHandler(IChannelRepository channelRepository, IMapper mapper)
    {
        _channelRepository = channelRepository;
        _mapper = mapper;
    }

    protected override async Task<ApiResult<ChannelInformation>> Handle(ChannelUpdateCommand request, CancellationToken cancellationToken)
    {
        var channel = await _channelRepository.FindByIdAsync(request.Id) ?? throw new EntityNotFoundException<Channel>("短信通道不存在");
        var newChannel = _mapper.Map<Channel>(request); // TODO map ChannelUpdateCommand => Channel

        foreach (var property in typeof(Channel).GetProperties(BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty))
        {
            var value = property.GetValue(newChannel);

            if (value is not null)
            {
                property.SetValue(channel, value);
            }
        }

        await _channelRepository.UnitWork.SaveEntitiesAsync(cancellationToken);

        return ApiResultFactory.Success(_mapper.Map<ChannelInformation>(channel));
    }
}
