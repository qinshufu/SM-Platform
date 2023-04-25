using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmPlatform.Domain.DataModels;

namespace SmPlatform.Instructure.EntityFramework.EntityConfigurations;

/// <summary>
/// 消息接受日志类型配置
/// </summary>
public class MessageReceivelLogTypeConfiguration : IEntityTypeConfiguration<MessageReceiveLog>
{
    public void Configure(EntityTypeBuilder<MessageReceiveLog> builder)
    {
        builder.ToTable(nameof(MessageReceiveLog));

        builder.Ignore(nameof(MessageReceiveLog.RequestParams));

        builder.Ignore(nameof(MessageReceiveLog.Configurations));

        builder.Property("_requestParams").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName(nameof(MessageReceiveLog.RequestParams)).HasDefaultValue("{}");

        builder.Property("_configurations").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName(nameof(MessageReceiveLog.Configurations)).HasDefaultValue("[]");
    }
}
