using Microsoft.EntityFrameworkCore;
using SmPlatform.Domain;
using SmPlatform.Domain.DataModels;

namespace SmPlatform.Instructure.EntityFramework.Repositories;

/// <summary>
/// 仓储抽象
/// </summary>
public class AbstractRepository<T> : IRepository<T> where T : Entity
{
    protected readonly SmsDbContext _dbContext;

    public AbstractRepository(SmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IUnitWork UnitWork => _dbContext;

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default) =>
        (await _dbContext.Set<T>().AddAsync(entity, cancellationToken)).Entity;

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _ = (await _dbContext.Set<T>().Where(t => t.Id == id).ExecuteDeleteAsync(cancellationToken));

    public Task<T?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _dbContext.Set<T>().SingleOrDefaultAsync(t => t.Id == id, cancellationToken);

    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default) =>
        Task.FromResult(_dbContext.Set<T>().Update(entity).Entity);
}
