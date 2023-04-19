namespace SmPlatform.Model.DataModels;

/// <summary>
/// 签名
/// </summary>
public class Signature : Entity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 签名编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 签名内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 签名备注
    /// </summary>
    public string Remark { get; set; }
}
