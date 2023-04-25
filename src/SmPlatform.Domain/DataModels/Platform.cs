namespace SmPlatform.Domain.DataModels;

/// <summary>
/// 接入平台
/// </summary>
public record Platform : Entity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 认证 key 
    /// </summary>
    public string AccessKeyId { get; set; }

    /// <summary>
    /// 认证 密钥
    /// </summary>
    public string AccessKeySecret { get; set; }

    /// <summary>
    /// ip
    /// </summary>
    public string IP { get; set; }

    /// <summary>
    /// 是否需要鉴权
    /// </summary>
    public bool NeedAuthentication { get; set; }

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsActive { get; set; } = false;

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 级别
    /// </summary>
    public int Level { get; set; }
}
