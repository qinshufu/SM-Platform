using MassTransit.Mediator;

namespace SmPlatform.Api.Application.Commands;

/// <summary>
/// 删除指定短信通道命令
/// </summary>
public class ChannelDeleteCommand : Request<ApiResult>
{
    /// <summary>
    /// 短信通道 ID
    /// </summary>
    public Guid Id { get; set; }
}
