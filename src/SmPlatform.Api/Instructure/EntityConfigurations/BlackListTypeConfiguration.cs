using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmPlatform.Model.DataModels;

namespace SmPlatform.ManagementApi.Instructure.EntityConfigurations;

public class BlackListTypeConfiguration : IEntityTypeConfiguration<BlackList>
{
    public void Configure(EntityTypeBuilder<BlackList> builder)
    {
        builder.ToTable(nameof(BlackList));
    }
}
