using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmPlatform.Model.DataModels;

namespace SmPlatform.ManagementApi.Instructure.EntityConfigurations;

public class EntityTypeConfiguration : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.ToTable(nameof(Entity));
    }
}
