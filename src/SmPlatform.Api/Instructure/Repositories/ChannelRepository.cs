using MassTransit.Internals;
using Microsoft.EntityFrameworkCore;
using SmPlatform.Api.Application.Exceptions;
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

    public override async Task<Channel?> GetOrDefaultByIdAsync(Guid id)
    {

        var channel = await base.GetOrDefaultByIdAsync(id);

        if (channel is null)
            return null;

        await _dbContext.Entry(channel).Collection(c => c.Templates).LoadAsync();
        await _dbContext.Entry(channel).Collection(c => c.Signatures).LoadAsync();

        return channel;
    }
}
