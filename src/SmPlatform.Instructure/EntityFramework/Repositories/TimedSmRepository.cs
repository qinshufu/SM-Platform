using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;

namespace SmPlatform.Instructure.EntityFramework.Repositories;

public class TimedSmRepository : AbstractRepository<TimedMessage>, ITimedSmRepository
{
    public TimedSmRepository(SmsDbContext dbContext) : base(dbContext)
    {
    }
}
