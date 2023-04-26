using SmPlatform.Domain;
using SmPlatform.Domain.DataModels;
using SmPlatform.Domain.Repositories;

namespace SmPlatform.Instructure.EntityFramework.Repositories;

/// <summary>
/// 手动处理日志仓库
/// </summary>
public class ManualProcessRepository : AbstractRepository<ManualProcess>, IManualProcessRepository
{
    public ManualProcessRepository(SmsDbContext dbContext) : base(dbContext)
    {
    }
}
