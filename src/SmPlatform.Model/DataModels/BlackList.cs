namespace SmPlatform.Model.DataModels;

/// <summary>
/// 黑名单
/// </summary>
public record BlackList : Entity
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public MessageCategory Category { get; set; }

    /// <summary>
    /// 账号：如果消息类型为短信则为手机号，邮件为邮箱地址，微信为微信号码
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 消息分类
/// </summary>
public enum MessageCategory
{
    /// <summary>
    /// 短信
    /// </summary>
    ShortMessage = 0,

    /// <summary>
    /// 邮件
    /// </summary>
    Email,

    /// <summary>
    /// 微信
    /// </summary>
    WeChat,
}