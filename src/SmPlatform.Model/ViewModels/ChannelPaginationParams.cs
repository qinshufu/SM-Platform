namespace SmPlatform.Model.ViewModels;

/// <summary>
/// 短信通道分页参数
/// </summary>
public record ChannelPaginationParams : IPaginationParams
{
    /// <summary>
    /// 时间范围的起始时间
    /// </summary>
    public DateTime StartTime { get; set; } = default;

    /// <summary>
    /// 时间范围的结束时间
    /// </summary>
    public DateTime EndTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 页码
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// 页大小
    /// </summary>
    public int PageSize { get; set; }
}
