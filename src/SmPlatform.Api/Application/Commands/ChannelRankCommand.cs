using MassTransit.Mediator;

namespace SmPlatform.ManagementApi.Application.Commands;

/// <summary>
/// 短信通道重新排序命令
/// </summary>
public record ChannelRankCommand : Request<ApiResult>
{
    /// <summary>
    /// 新顺序
    /// </summary>
    public List<Guid> Rank { get; set; }
}
