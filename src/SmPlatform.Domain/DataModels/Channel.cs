using System.Collections.Specialized;
using System.Text.Json;

namespace SmPlatform.Domain.DataModels;

/// <summary>
/// 短信通道配置
/// </summary>
public record Channel : Entity
{

    private string _otherOptions;

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
    /// 其他配置
    /// </summary>
    public NameValueCollection OtherOptions
    {
        get => JsonSerializer.Deserialize<NameValueCollection>(_otherOptions ?? "{}")!;
        set => _otherOptions = JsonSerializer.Serialize(value);
    }

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

/// <summary>
/// 消息内容类型
/// </summary>
public enum MessageValueType
{
    /// <summary>
    /// 文字
    /// </summary>
    Text = 0,

    /// <summary>
    /// 语音
    /// </summary>
    Voice,

    /// <summary>
    /// 推送
    /// </summary>
    Push
}