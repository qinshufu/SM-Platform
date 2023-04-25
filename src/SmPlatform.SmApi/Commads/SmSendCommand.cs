using MediatR;
using System;
using System.Collections.Specialized;

namespace SmPlatform.SmApi.Commads;

/// <summary>
/// 短信发送命令
/// </summary>
public class SmSendCommand : IRequest<CommandResult>
{
    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// 模板
    /// </summary>
    public Guid Template { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    public Guid Signature { get; set; }

    /// <summary>
    /// 模板参数
    /// </summary>
    public NameValueCollection TemplateParams { get; set; }

    /// <summary>
    /// 接入 KEY
    /// </summary>
    public string AccessKeyId { get; set; }

    /// <summary>
    /// 认证值
    /// </summary>
    public string AccessKeySecret { get; set; }

    /// <summary>
    /// 消息创建时间
    /// </summary>
    public DateTime CreateTime { get; init; } = DateTime.Now;

    /// <summary>
    /// 计划发送时间
    /// </summary>
    public DateTime? Timing { get; set; }
}
