using Microsoft.EntityFrameworkCore;
using SmPlatform.Domain.DataModels;

namespace SmPlatform.Api.Test;

public class SoftDeleteTest : NeedDbContext
{

    [Fact]
    public async Task ExistsEntityQueryTest()
    {
        var blackList = new BlackList() { IsDeleted = true, Account = "" };

        DbContext.Add(blackList);
        await DbContext.SaveChangesAsync();

        Assert.False(await DbContext.BlackLists.AnyAsync());

        blackList.IsDeleted = false;
        await DbContext.SaveChangesAsync();

        Assert.True(await DbContext.BlackLists.AnyAsync());
    }
}