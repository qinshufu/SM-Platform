using StackExchange.Redis;

namespace SmPlatform.Instructure.Distribution;

public class RedisLockManager : IDistributedLockManager
{
    private readonly IDatabase _db;

    public RedisLockManager(ConnectionMultiplexer multiplexer)
    {
        _db = multiplexer.GetDatabase(0);
    }

    public bool Lock(string id, out IDistributedLock @lock)
    {
        @lock = new RedisLock(id, Guid.NewGuid().ToString());

        return _db.StringSet(@lock.Key, @lock.Value, expiry: TimeSpan.FromMinutes(10), when: When.NotExists);
    }

    public bool Unlock(IDistributedLock @lock)
    {
        var result = _db.ScriptEvaluate("""
            local value = redis.call('GET', KEYS[1])
            if value == ARGV[1] then
                redis.call('DEL', KEYS[1])
                return true
            else
                return false
            end
            """, keys: new RedisKey[] { @lock.Key }, values: new RedisValue[] { @lock.Value });

        return result.IsNull is false;
    }
}
