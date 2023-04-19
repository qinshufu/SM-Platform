namespace SmPlatform.Model.ViewModels;

/// <summary>
/// 分页参数
/// </summary>
public interface IPaginationParams
{
    /// <summary>
    /// 页码
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// 分页参数
    /// </summary>
    public int PageSize { get; set; }
}
