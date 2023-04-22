using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmPlatform.Domain.DataModels;

namespace SmPlatform.Instructure.EntityFramework.EntityConfigurations;

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

        builder.HasMany(c => c.Templates).WithMany().UsingEntity(b =>
        {
            b.Property<Guid>("ChannelId");
            b.Property<Guid?>("TemplateId");

            b.HasKey("ChannelId", "TemplateId");

            b.HasOne(typeof(Channel).FullName!, null)
                .WithMany()
                .HasForeignKey("ChannelId")
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(typeof(Template).FullName!, null)
                .WithMany()
                .HasForeignKey("TemplateId")
                .OnDelete(DeleteBehavior.ClientSetNull);

        });

        builder.HasMany(c => c.Signatures).WithMany().UsingEntity(b =>
        {
            b.Property<Guid>("ChannelId");
            b.Property<Guid?>("SignatureId");

            b.HasKey("ChannelId", "SignatureId");

            b.HasOne(typeof(Channel).FullName!, null)
                .WithMany()
                .HasForeignKey("ChannelId")
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(typeof(Signature).FullName!, null)
                .WithMany()
                .HasForeignKey("SignatureId")
                .OnDelete(DeleteBehavior.ClientSetNull);

        });

        builder.HasOne(c => c.Platform).WithMany().OnDelete(DeleteBehavior.Cascade);
    }
}
