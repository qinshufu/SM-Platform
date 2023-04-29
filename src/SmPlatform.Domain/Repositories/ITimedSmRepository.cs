using SmPlatform.Domain.DataModels;
using System.Linq.Expressions;

namespace SmPlatform.Domain.Repositories;

/// <summary>
/// 定时消息仓库
/// </summary>
public interface ITimedSmRepository : IRepository<TimedMessage>
{
    /// <summary>
    /// 查找定时消息
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    Task<List<TimedMessage>> FindAsync(Expression<Func<TimedMessage, bool>> value);
}
