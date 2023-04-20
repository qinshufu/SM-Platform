using SmPlatform.Model.DataModels;
using System.Collections.Specialized;

namespace SmPlatform.Model.ViewModels;

/// <summary>
/// 频道信息
/// </summary>
public class ChannelInformation : EntityBasicInfo
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
    /// 短信模板
    /// </summary>
    public List<Template> Templates { get; set; }

    /// <summary>
    /// 短信列表
    /// </summary>
    public List<Signature> Signatures { get; set; }

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
