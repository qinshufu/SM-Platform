using Microsoft.EntityFrameworkCore;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;
using System.Linq.Expressions;

namespace SmPlatform.Instructure.EntityFramework.Repositories;

/// <summary>
/// 黑名单仓储
/// </summary>
public class BlackListRepository : AbstractRepository<BlackList>, IBlackListRepository
{
    public BlackListRepository(SmsDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> ExistsAsync(Expression<Func<BlackList, bool>> predicator) =>
        _dbContext.Set<BlackList>().AnyAsync(predicator);
}
