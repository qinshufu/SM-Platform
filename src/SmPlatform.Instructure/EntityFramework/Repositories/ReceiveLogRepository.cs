using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;

namespace SmPlatform.Instructure.EntityFramework.Repositories;

public class ReceiveLogRepository : AbstractRepository<MessageReceiveLog>, IReceiveLogRepository
{
    public ReceiveLogRepository(SmsDbContext dbContext) : base(dbContext)
    {
    }
}
