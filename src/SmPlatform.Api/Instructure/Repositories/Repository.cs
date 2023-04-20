using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using SmPlatform.Api.Domain;
using SmPlatform.Model.DataModels;

namespace SmPlatform.Api.Instructure.Repositories;

/// <summary>
/// 仓储的默认实现
/// </summary>
public abstract class Repository<T> : IRepository<T>
    where T : Entity
{
    protected readonly SmsDbContext _dbContext;


    public Repository(SmsDbContext dbContext)
    {
        UnitWork = dbContext;
        _dbContext = dbContext;
    }

    public IUnitWork UnitWork { get; init; }

    public virtual async Task<T> AddAsync(T entity) => (await _dbContext.Set<T>().AddAsync(entity!)).Entity;

    public virtual async Task DeleteByIdAsync(Guid id)
    {
        await _dbContext.Set<T>().Where(t => t.Id == id).ExecuteDeleteAsync();
    }

    public virtual Task<T?> GetOrDefaultByIdAsync(Guid id) => _dbContext.Set<T>().SingleOrDefaultAsync(t => t.Id == id);

    public virtual Task<T> UpdateAsync(T entity)
    {
        var value = _dbContext.Set<T>().Update(entity).Entity;
        return Task.FromResult(value);
    }
}
