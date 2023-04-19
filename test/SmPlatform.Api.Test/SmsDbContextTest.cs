using SmPlatform.Model.DataModels;

namespace SmPlatform.Api.Test;

public class SmsDbContextTest : NeedDbContext
{

    [Fact]
    public async Task CreateTimeSetTest()
    {
        var blackList = new BlackList() { IsDeleted = true, Account = "" };

        DbContext.Add(blackList);
        await DbContext.SaveEntitiesAsync();

        Assert.True(DateTime.UtcNow - blackList.CreateTime < TimeSpan.FromMilliseconds(200));
    }

    [Fact]
    public async Task UpdateTimeSetTest()
    {
        var blackList = new BlackList() { IsDeleted = true, Account = "" };

        DbContext.Add(blackList);
        await DbContext.SaveEntitiesAsync();

        blackList.Account = "test@test.com";
        await DbContext.SaveEntitiesAsync();

        Assert.True(DateTime.UtcNow - blackList.UpdateTime < TimeSpan.FromMilliseconds(200));
    }
}
