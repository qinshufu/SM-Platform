using MassTransit.Mediator;
using SmPlatform.ManagementApi.Domain.Repositories;

namespace SmPlatform.ManagementApi.Application.Commands;

/// <summary>
/// 短信通道删除命令
/// </summary>
public class ChannelDeleteHandler : MediatorRequestHandler<ChannelDeleteCommand, ApiResult>
{
    private readonly IChannelRepository _channelRepository;

    public ChannelDeleteHandler(IChannelRepository channelRepository)
    {
        _channelRepository = channelRepository;
    }

    protected override async Task<ApiResult> Handle(ChannelDeleteCommand request, CancellationToken cancellationToken)
    {
        await _channelRepository.DeleteByIdAsync(request.Id, cancellationToken);
        await _channelRepository.UnitWork.SaveEntitiesAsync(cancellationToken);

        return ApiResultFactory.SuccessWithoutData();
    }
}
