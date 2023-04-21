using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmPlatform.Model.DataModels;

namespace SmPlatform.ManagementApi.Instructure.EntityConfigurations;

public class SignatureTypeConfiguration : IEntityTypeConfiguration<Signature>
{
    public void Configure(EntityTypeBuilder<Signature> builder)
    {
        builder.ToTable(nameof(Signature));
    }
}
