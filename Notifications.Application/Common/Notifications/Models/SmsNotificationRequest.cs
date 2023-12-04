using Notifications.Domain.Enums;

namespace Notifications.Application.Common.Notifications.Models;

public class SmsNotificationRequest : NotificationRequest
{
    public SmsNotificationRequest() => Type = NotificationType.Sms;

}