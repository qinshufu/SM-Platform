namespace SmPlatform.ManagementApi.Domain;

/// <summary>
/// 工作单元
/// </summary>
public interface IUnitWork
{
    /// <summary>
    /// 保存所有实体，和 SaveChangesAsync 不同的是会携带其他的副作用
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 保存所有修改
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}