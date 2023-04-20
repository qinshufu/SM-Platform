namespace SmPlatform.Model.ViewModels;

/// <summary>
/// 实体基础信息
/// </summary>
public class EntityBasicInfo
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}
