using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmPlatform.Domain.DataModels;

namespace SmPlatform.Instructure.EntityFramework.EntityConfigurations;

/// <summary>
/// 消息发送日志类型设置
/// </summary>
public class MesageSendLogTypeConfiguration : IEntityTypeConfiguration<MessageSendLog>
{
    public void Configure(EntityTypeBuilder<MessageSendLog> builder)
    {
        builder.ToTable(nameof(MessageSendLog));

        builder.Ignore(nameof(MessageSendLog.RequestParams));

        builder.Ignore(nameof(MessageSendLog.ResponseParams));

        builder.Property("_requestParams").UsePropertyAccessMode(PropertyAccessMode.Field).HasDefaultValue("{}").HasColumnName(nameof(MessageSendLog.RequestParams));

        builder.Property("_responseParams").UsePropertyAccessMode(PropertyAccessMode.Field).HasDefaultValue("{}").HasColumnName(nameof(MessageSendLog.ResponseParams));
    }
}
