using Microsoft.EntityFrameworkCore;
using SmPlatform.ManagementApi.Domain;
using SmPlatform.Model.DataModels;

namespace SmPlatform.ManagementApi.Instructure;

public class SmsDbContext : DbContext, IUnitWork
{
    public DbSet<BlackList> BlackLists { get; set; }

    public DbSet<Channel> Configurations { get; set; }

    public DbSet<ManualProcess> ManualProcesses { get; set; }

    public DbSet<MessageReceiveLog> MessageReceiveLogs { get; set; }

    public DbSet<MessageSendLog> MessageSendLogs { get; set; }

    public DbSet<Platform> Platforms { get; set; }

    public DbSet<Signature> Signatures { get; set; }

    public DbSet<Template> Templates { get; set; }

    public DbSet<TimedMessage> TimedMessages { get; set; }

    public SmsDbContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// 保存所有 entity
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        var changedEntities = this.ChangeTracker.Entries<Entity>();

        foreach (var entity in changedEntities)
        {
            if (entity.State is EntityState.Modified)
                entity.Entity.UpdateTime = DateTime.UtcNow;

            if (entity.State is EntityState.Added)
                entity.Entity.CreateTime = DateTime.UtcNow;
        }

        await SaveChangesAsync(cancellationToken);

        return true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 下面这行代码在使用 ef 命令时将无法获取预期的 assembly
        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetEntryAssembly()!);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);

        modelBuilder.Entity<Entity>().HasQueryFilter(e => e.IsDeleted == false);
    }
}
