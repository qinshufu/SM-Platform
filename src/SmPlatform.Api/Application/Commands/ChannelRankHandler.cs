using MassTransit.Mediator;
using SmPlatform.ManagementApi.Domain.Repositories;

namespace SmPlatform.ManagementApi.Application.Commands;

/// <summary>
/// 短信通道信息重新排序命令处理器
/// </summary>
public class ChannelRankHandler : MediatorRequestHandler<ChannelRankCommand, ApiResult>
{
    private readonly IChannelRepository _channelRepository;

    public ChannelRankHandler(IChannelRepository channelRepository)
    {
        _channelRepository = channelRepository;
    }

    protected override async Task<ApiResult> Handle(ChannelRankCommand request, CancellationToken cancellationToken)
    {
        if (request.Rank.ToHashSet().Count != request.Rank.Count)
        {
            return ApiResultFactory.FailWithoutData("重复的短信通道");
        }

        if (await _channelRepository.CountAsync(cancellationToken) != request.Rank.Count())
        {
            return ApiResultFactory.FailWithoutData("没有列出所有通道，无法完成重新排序的操作");
        }

        var rankList = request.Rank.Select((r, index) => (Id: r, Rank: index + 1));
        var channels = await _channelRepository.GetAllAsync(cancellationToken);


        foreach (var (rank, channel) in from r in rankList
                                        join c in channels on r.Id equals c.Id
                                        select (r.Rank, c))
        {
            channel.Level = rank;
        }

        await _channelRepository.UnitWork.SaveEntitiesAsync(cancellationToken);

        return ApiResultFactory.SuccessWithoutData();
    }
}
