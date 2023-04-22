using SmPlatform.Domain.DataModels;
using System.Linq.Expressions;

namespace SmPlatform.Domain.Repositories;

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
    /// 根据条件判断制定通道是否存在
    /// </summary>
    /// <returns></returns>
    Task<bool> ExistsAsync(Expression<Func<Channel, bool>> expression);

    /// <summary>
    /// 根据条件获取短信通道
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    Task<Channel> FindAsync(Expression<Func<Channel, bool>> value);

    /// <summary>
    /// 获取全部的通道实体
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Channel>> GetAllAsync(CancellationToken cancellationToken = default);
}
