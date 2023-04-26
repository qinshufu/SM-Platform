using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;

namespace SmPlatform.Instructure.EntityFramework.Repositories;

public class SendLogRepository : AbstractRepository<MessageSendLog>, ISendLogRepository
{
    public SendLogRepository(SmsDbContext dbContext) : base(dbContext)
    {
    }
}
