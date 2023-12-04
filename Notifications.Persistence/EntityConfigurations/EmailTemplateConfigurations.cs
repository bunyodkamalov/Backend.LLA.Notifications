using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notifications.Domain.Entities;

namespace Notifications.Persistence.EntityConfigurations;

public class EmailTemplateConfigurations : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder.Property(emailTemplate => emailTemplate.Subject).IsRequired().HasMaxLength(256);
    }
}