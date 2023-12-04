using Notifications.Domain.Enums;

namespace Notifications.Application.Common.Notifications.Models;

public class EmailNotificationRequest : NotificationRequest
{
    public EmailNotificationRequest() => Type = NotificationType.Email;
}