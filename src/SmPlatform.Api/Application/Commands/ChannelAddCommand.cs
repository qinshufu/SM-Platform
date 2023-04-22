using MassTransit.Mediator;
using SmPlatform.Domain.DataModels;
using SmPlatform.Model.ViewModels;
using System.Collections.Specialized;

namespace SmPlatform.ManagementApi.Application.Commands;

/// <summary>
/// 添加通道命令
/// </summary>
public class ChannelAddCommand : Request<ApiResult<ChannelInformation>>
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 平台
    /// </summary>
    public Platform Platform { get; set; }

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
