using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmPlatform.Domain.DataModels;

namespace SmPlatform.Instructure.EntityFramework.EntityConfigurations;

public class SignatureTypeConfiguration : IEntityTypeConfiguration<Signature>
{
    public void Configure(EntityTypeBuilder<Signature> builder)
    {
        builder.ToTable(nameof(Signature));
    }
}
