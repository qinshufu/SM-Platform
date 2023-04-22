using Microsoft.EntityFrameworkCore;
using SmPlatform.Instructure.EntityFramework;

namespace SmPlatform.Api.Test;

/// <summary>
/// 需要数据库的测试的抽象
/// </summary>
public abstract class NeedDbContext : IAsyncDisposable
{
    /// <summary>
    /// 这里的文件位置应该每次都不一样，以免一部测试的时候因为数据库起冲突
    /// </summary>
    public readonly string DbFilePath = Path.GetTempFileName();

    public SmsDbContext DbContext { get; }

    public NeedDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<SmsDbContext>().UseSqlite("Data Source=" + DbFilePath + ";");
        var dbContext = new SmsDbContext(optionsBuilder.Options);

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        DbContext = dbContext;
    }

    public ValueTask DisposeAsync()
    {
        File.Delete(DbFilePath);
        return ValueTask.CompletedTask;
    }
}
