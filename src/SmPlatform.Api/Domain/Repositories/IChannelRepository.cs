using SmPlatform.Model.DataModels;

namespace SmPlatform.ManagementApi.Domain.Repositories;

/// <summary>
/// 通道仓储
/// </summary>
public interface IChannelRepository : IRepository<Channel>
{
    /// <summary>
    /// 返回通道实体总数
    /// </summary>
    /// <returns></returns>
    Task<long> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取全部的通道实体
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Channel>> GetAllAsync(CancellationToken cancellationToken = default);
}
