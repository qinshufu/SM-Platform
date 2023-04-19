using System.Runtime.CompilerServices;

namespace SmPlatform.Model.ViewModels;

/// <summary>
/// 排序参数
/// </summary>
public interface IOrderParams
{
    /// <summary>
    /// 排序字段
    /// </summary>
    List<string> OrderBy { get; set; }
}

/// <summary>
/// 如何排序
/// </summary>
public record struct OrderBy(
    /// <summary>
    /// 排序字段
    /// </summary>
    string FieldName,

    /// <summary>
    /// 升序降序？
    /// </summary>
    Order Order);

public enum Order
{
    /// <summary>
    /// 升序
    /// </summary>
    Ascending,

    /// <summary>
    /// 降序
    /// </summary>
    Descending
}

/// <summary>
/// 排序参数扩展
/// </summary>
public static class OrderParamsExtension
{
    /// <summary>
    /// 升序
    /// </summary>
    public const string ASC = "ASC";

    /// <summary>
    /// 降序
    /// </summary>
    public const string DESC = "DESC";

    /// <summary>
    /// 获取排序字段
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    public static List<OrderBy> GetOrders(this IOrderParams @params) => @params.OrderBy
        .Select(s => s.Trim().Split(',').Where(s => string.IsNullOrEmpty(s)).ToArray())
        .Where(spans => spans.Any())
        .Select(spans => (spans[0], spans.Length > 1 ? spans[1] : DESC))
        .Select(spans => new OrderBy(spans.Item1, ASC.Equals(spans.Item2, StringComparison.CurrentCultureIgnoreCase) ? Order.Ascending : Order.Descending))
        .ToList();
}
