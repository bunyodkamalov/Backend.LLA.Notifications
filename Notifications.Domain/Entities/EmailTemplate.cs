using Notifications.Domain.Enums;

namespace Notifications.Domain.Entities;

public class EmailTemplate : NotificationTemplate
{
    public EmailTemplate()
    {
        Type = NotificationType.Email;
    }

    public string Subject { get; set; } = default!;
}