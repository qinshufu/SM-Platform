using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmPlatform.Model.DataModels;

namespace SmPlatform.Api.Instructure.EntityConfigurations;

/// <summary>
/// 平台配置实体的配置
/// </summary>
public class ConfigurationTypeConfiguration : IEntityTypeConfiguration<Configuration>
{
    public void Configure(EntityTypeBuilder<Configuration> builder)
    {
        builder.ToTable(nameof(Configuration));

        builder.Ignore(nameof(Configuration.OtherOptions));

        builder.Property("_otherOptions").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName(nameof(Configuration.OtherOptions)).HasDefaultValue("{}");
    }
}
