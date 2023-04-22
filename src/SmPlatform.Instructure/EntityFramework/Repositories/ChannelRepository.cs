using Microsoft.EntityFrameworkCore;
using SmPlatform.Domain;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;
using System.Linq.Expressions;

namespace SmPlatform.Instructure.EntityFramework.Repositories;

/// <summary>
/// 通道仓储
/// </summary>
public class ChannelRepository : IChannelRepository
{
    private readonly SmsDbContext _dbContext;

    public ChannelRepository(SmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IUnitWork UnitWork => _dbContext;

    public async Task<Channel> AddAsync(Channel entity, CancellationToken cancellationToken = default) =>
        (await _dbContext.AddAsync(entity, cancellationToken)).Entity;

    public Task<long> CountAsync(CancellationToken cancellationToken = default) => _dbContext.Set<Channel>().LongCountAsync(cancellationToken);

    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _dbContext.Set<Channel>().Where(c => c.Id == id).ExecuteDeleteAsync();

    public async Task<List<Channel>> GetAllAsync(CancellationToken cancellationToken = default)
    {

        var channels = await _dbContext.Set<Channel>().ToListAsync(cancellationToken);

        foreach (var channel in channels)
        {
            await _dbContext.Entry(channel).Collection(c => c.Templates).LoadAsync();
            await _dbContext.Entry(channel).Collection(c => c.Signatures).LoadAsync();
        }

        return channels;
    }

    public async Task<Channel?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {

        var channel = await _dbContext.Set<Channel>().SingleOrDefaultAsync(c => c.Id == id);

        if (channel is null)
            return null;

        await _dbContext.Entry(channel).Collection(c => c.Templates).LoadAsync();
        await _dbContext.Entry(channel).Collection(c => c.Signatures).LoadAsync();

        return channel;
    }

    public Task<Channel> UpdateAsync(Channel entity, CancellationToken cancellationToken = default) =>
        Task.FromResult(_dbContext.Update(entity).Entity);

    /// <summary>
    /// 根据条件判断是否存在
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<bool> ExistsAsync(Expression<Func<Channel, bool>> expression) => _dbContext.Set<Channel>().AnyAsync(expression);

    public Task<Channel> FindAsync(Expression<Func<Channel, bool>> value) => _dbContext.Set<Channel>().FirstAsync(value);
}
