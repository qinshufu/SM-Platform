using MassTransit.Mediator;
using SmPlatform.Model.ViewModels;

namespace SmPlatform.Api.Application.Commands;

/// <summary>
/// 更新通道与模板签名绑定的命令
/// </summary>
public record ChannelBindingUpdateCommand : Request<ApiResult<ChannelInformation>>
{
    /// <summary>
    /// 通道 ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 模板 ID
    /// </summary>
    public List<Guid> Templates { get; set; }

    /// <summary>
    /// 签名 ID
    /// </summary>
    public List<Guid> Signatures { get; set; }
}
