using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmPlatform.Domain.DataModels;

namespace SmPlatform.Instructure.EntityFramework.EntityConfigurations;

/// <summary>
/// 手工处理记录的配置
/// </summary>
public class ManualProcessTypeConfiguration : IEntityTypeConfiguration<ManualProcess>
{
    public void Configure(EntityTypeBuilder<ManualProcess> builder)
    {
        builder.ToTable(nameof(ManualProcess));

        builder.Ignore(nameof(ManualProcess.Channels));

        builder.Property("_channels").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName(nameof(ManualProcess.Channels)).HasDefaultValue("[]");
    }
}
