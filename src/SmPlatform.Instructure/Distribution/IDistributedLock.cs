namespace SmPlatform.Instructure.Distribution;

/// <summary>
/// 分布式锁
/// </summary>
public interface IDistributedLock
{
    /// <summary>
    /// 标识锁
    /// </summary>
    string Key { get; }

    /// <summary>
    /// 标识谁上的锁
    /// </summary>
    string Value { get; }
}

public record struct DistributedLock : IDistributedLock
{
    public string Key { get; init; }

    public string Value { get; init; }
}
