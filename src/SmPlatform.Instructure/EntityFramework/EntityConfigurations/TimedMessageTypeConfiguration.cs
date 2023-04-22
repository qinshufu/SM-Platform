using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmPlatform.Domain.DataModels;

namespace SmPlatform.Instructure.EntityFramework.EntityConfigurations;

/// <summary>
/// 定时消息模型配置
/// </summary>
public class TimedMessageTypeConfiguration : IEntityTypeConfiguration<TimedMessage>
{
    public void Configure(EntityTypeBuilder<TimedMessage> builder)
    {
        builder.ToTable(nameof(TimedMessage));

        builder.Ignore(nameof(TimedMessage.RequestParams));

        builder.Property("_requestParams").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName(nameof(TimedMessage.RequestParams)).HasDefaultValue("{}");
    }
}
