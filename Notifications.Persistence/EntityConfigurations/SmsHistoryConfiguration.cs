using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.Entities;

namespace Notifications.Persistence.EntityConfigurations;

public class SmsHistoryConfiguration : IEntityTypeConfiguration<SmsHistory>
{
    public void Configure(EntityTypeBuilder<SmsHistory> builder)
    {
        builder.Property(temp => temp.ReceiverPhoneNumber).IsRequired().HasMaxLength(32);
        builder.Property(temp => temp.SenderPhoneNumber).IsRequired().HasMaxLength(32);
    }
}