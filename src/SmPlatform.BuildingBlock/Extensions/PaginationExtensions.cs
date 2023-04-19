namespace SmPlatform.BuildingBlock.Extensions;

/// <summary>
/// 分页
/// </summary>
public record Pagination<T> where T : notnull
{
    /// <summary>
    /// 分页的原始数据总数
    /// </summary>
    public long Total { get; init; }

    /// <summary>
    /// 页码
    /// </summary>
    public int PageNumber { get; init; }

    /// <summary>
    /// 页大小
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 当前分页中的数据
    /// </summary>
    public IEnumerable<T> Items { get; init; } = new T[0];
}

/// <summary>
/// 支持分页的扩展方法
/// </summary>
public static class PaginationExtensions
{
    /// <summary>
    /// 基于排序后枚举的分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static Pagination<T> Pagination<T>(this IOrderedEnumerable<T> source, int pageNumber, int pageSize) where T : notnull =>
        new Pagination<T> { PageNumber = pageNumber, PageSize = pageSize, Items = source.Skip(pageNumber * pageSize - pageSize), Total = source.LongCount() };

    /// <summary>
    /// 基于排序后查询的分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static Pagination<T> Pagination<T>(this IOrderedQueryable<T> source, int pageNumber, int pageSize) where T : notnull =>
        new Pagination<T> { PageNumber = pageNumber, PageSize = pageSize, Items = source.Skip(pageNumber * pageSize - pageSize), Total = source.LongCount() };
}

