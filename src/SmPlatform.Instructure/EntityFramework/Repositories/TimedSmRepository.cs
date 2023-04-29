using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;

namespace SmPlatform.Instructure.EntityFramework.Repositories;

public class TimedSmRepository : AbstractRepository<TimedMessage>, ITimedSmRepository
{
    public TimedSmRepository(SmsDbContext dbContext) : base(dbContext)
    {
    }

    public Task<List<TimedMessage>> FindAsync(Expression<Func<TimedMessage, bool>> predicator) =>
        _dbContext.Set<TimedMessage>().Where(predicator).ToListAsync();
}
