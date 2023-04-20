using System.Runtime.InteropServices;

namespace SmPlatform.Api.Domain;

/// <summary>
/// 仓储
/// </summary>
public interface IRepository<T>
    where T : class
{
    /// <summary>
    /// 工作单元
    /// </summary>
    IUnitWork UnitWork { get; }

    /// <summary>
    /// 根据 ID 查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> GetOrDefaultByIdAsync(Guid id);

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// 删除实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteByIdAsync(Guid id);

    /// <summary>
    /// 添加实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> AddAsync(T entity);
}
