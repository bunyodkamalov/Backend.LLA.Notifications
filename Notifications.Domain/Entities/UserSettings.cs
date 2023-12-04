using Notifications.Domain.Common.Entities;
using Notifications.Domain.Enums;

namespace Notifications.Domain.Entities;

public class UserSettings : IEntity
{
    public Guid Id { get; set; }
    
    public NotificationType? PreferredNotificationType { get; set; }
}