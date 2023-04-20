using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using SmPlatform.Api.Domain;
using SmPlatform.Model.DataModels;

namespace SmPlatform.Api.Application.Commands;

/// <summary>
/// 短信通道信息重新排序命令处理器
/// </summary>
public class ChannelRankHandler : MediatorRequestHandler<ChannelRankCommand, ApiResult>
{
    private readonly IChannelRepository _channelRepository;

    private readonly IQueryable<Channel> _channel;

    public ChannelRankHandler(IChannelRepository channelRepository, IQueryable<Channel> channels)
    {
        _channelRepository = channelRepository;
        _channel = channels;
    }

    protected override async Task<ApiResult> Handle(ChannelRankCommand request, CancellationToken cancellationToken)
    {
        if (await _channel.CountAsync(cancellationToken) != request.Rank.Count())
        {
            return ApiResultFactory.FailWithoutData("没有列出所有通道，无法完成重新排序的操作");
        }

        var rankList = request.Rank.Select((r, index) => (Id: r, Rank: index + 1));
        var channelsForUpdate = await _channel.Where(c => rankList.Select(r => r.Id).Contains(c.Id)).ToArrayAsync(cancellationToken);

        foreach (var (rank, channel) in from r in rankList
                                        join c in channelsForUpdate on r.Id equals c.Id
                                        select (r.Rank, c))
        {
            channel.Level = rank;
        }

        await _channelRepository.UnitWork.SaveEntitiesAsync(cancellationToken);

        return ApiResultFactory.SuccessWithoutData();
    }
}
