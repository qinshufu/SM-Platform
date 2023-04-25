namespace SmPlatform.Instructure.Distribution;

/// <summary>
/// 分布式锁管理器
/// </summary>
public interface IDistributedLockManager
{
    /// <summary>
    /// 上锁
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    bool Lock(string id, out IDistributedLock @lock);

    /// <summary>
    /// 解锁
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    bool Unlock(IDistributedLock @lock);
}
