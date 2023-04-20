using SmPlatform.Model.DataModels;

namespace SmPlatform.Api.Instructure.Repositories;

/// <summary>
/// 通道仓储
/// </summary>
public class ChannelRepository : Repository<Channel>
{
    public ChannelRepository(SmsDbContext dbContext) : base(dbContext)
    {
    }
}
