namespace SmPlatform.ManagementApi.Domain;

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
    Task<T?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 添加实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
}
