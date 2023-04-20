using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using SmPlatform.Api.Application.Exceptions;
using SmPlatform.Api.Domain;
using SmPlatform.Model.DataModels;
using SmPlatform.Model.ViewModels;
using System.Linq;

namespace SmPlatform.Api.Application.Commands;

/// <summary>
/// 短信通道更新命令处理器
/// </summary>
public class ChannelUpdateHandler : MediatorRequestHandler<ChannelUpdateCommand, ApiResult<ChannelInformation>>
{
    private readonly IChannelRepository _channelRepository;

    public ChannelUpdateHandler(IChannelRepository channelRepository)
    {
        _channelRepository = channelRepository;
    }

    protected override async Task<ApiResult<ChannelInformation>> Handle(ChannelUpdateCommand request, CancellationToken cancellationToken)
    {
        var channel = await _channelRepository.GetOrDefaultByIdAsync(request.Id) ?? throw new EntityNotFoundException<Channel>("短信通道不存在");

        // TODO
    }
}
