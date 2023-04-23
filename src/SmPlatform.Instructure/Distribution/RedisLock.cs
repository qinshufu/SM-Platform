namespace SmPlatform.Instructure.Distribution;

/// <summary>
/// Redis 锁
/// </summary>
public record struct RedisLock : IDistributedLock
{
    public RedisLock(string key, string value)
    {
        Key = key;
        Value = value;
    }

    public string Key { get; init; }

    public string Value { get; init; }
}
