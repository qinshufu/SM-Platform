namespace SmPlatform.Model.DataModels;

/// <summary>
/// 模板
/// </summary>
public record Template : Entity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 模板编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public TemplateCategory Category { get; set; }

}

/// <summary>
/// 模板分类
/// </summary>
public enum TemplateCategory
{
    /// <summary>
    /// 验证码
    /// </summary>
    VerificationCode = 0,

    /// <summary>
    /// 通知
    /// </summary>
    Notification,

    /// <summary>
    /// 推广
    /// </summary>
    Promotion
}