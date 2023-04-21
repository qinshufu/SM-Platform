using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmPlatform.Model.DataModels;

namespace SmPlatform.Api.Instructure.EntityConfigurations;

/// <summary>
/// 平台配置实体的配置
/// </summary>
public class ConfigurationTypeConfiguration : IEntityTypeConfiguration<Channel>
{
    public void Configure(EntityTypeBuilder<Channel> builder)
    {
        builder.ToTable(nameof(Channel));

        builder.Ignore(nameof(Channel.OtherOptions));

        builder.Property("_otherOptions").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName(nameof(Channel.OtherOptions)).HasDefaultValue("{}");

        builder.HasMany(c => c.Templates).WithMany(t => t.Channels);

        builder.HasMany(c => c.Signatures).WithMany(s => s.Channels);
    }
}
