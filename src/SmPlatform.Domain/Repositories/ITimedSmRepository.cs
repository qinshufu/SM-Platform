using SmPlatform.Domain.DataModels;

namespace SmPlatform.Domain.Repositories;

/// <summary>
/// 定时消息仓库
/// </summary>
public interface ITimedSmRepository : IRepository<TimedMessage>
{
}
