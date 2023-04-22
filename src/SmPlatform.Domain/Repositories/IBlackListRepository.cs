using SmPlatform.Domain.DataModels;
using System.Linq.Expressions;

namespace SmPlatform.Domain.Repositories;

/// <summary>
/// 黑名单列表仓储
/// </summary>
public interface IBlackListRepository : IRepository<BlackList>
{
    /// <summary>
    /// 判断指定黑名单是否存在
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    Task<bool> ExistsAsync(Expression<Func<BlackList, bool>> value);
}
