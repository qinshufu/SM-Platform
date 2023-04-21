using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmPlatform.Model.DataModels;

namespace SmPlatform.ManagementApi.Instructure.EntityConfigurations;

public class PlatformTypeConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder.ToTable(nameof(Platform));
    }
}
