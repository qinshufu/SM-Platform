using MassTransit.Mediator;
using SmPlatform.Model.DataModels;
using SmPlatform.Model.ViewModels;
using System.Collections.Specialized;

namespace SmPlatform.Api.Application.Commands;

/// <summary>
/// 更新短信通道命令
/// </summary>
public record ChannelUpdateCommand : Request<ApiResult<ChannelInformation>>
{
    public Guid Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 平台
    /// </summary>
    public Guid Platform { get; set; }

    /// <summary>
    /// 域名
    /// </summary>
    public string Domain { get; set; }

    /// <summary>
    /// 密钥
    /// </summary>
    public string AccessKeySecret { get; set; }

    /// <summary>
    /// 密钥 key
    /// </summary>
    public string AccessKeyId { get; set; }

    /// <summary>
    /// 其他配置
    /// </summary>
    public NameValueCollection OtherOptions { get; set; }

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 是否开启
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 级别
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 消息内容类型
    /// </summary>
    public MessageValueType MessageValueType { get; set; }
}
