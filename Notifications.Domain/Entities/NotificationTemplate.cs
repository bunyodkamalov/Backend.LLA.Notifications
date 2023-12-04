using Notifications.Domain.Common.Entities;
using Notifications.Domain.Enums;

namespace Notifications.Domain.Entities;

public abstract class NotificationTemplate : IEntity
{
    public Guid Id { get; set; }

    public string Content { get; set; } = default!;
    
    public NotificationType Type { get; set; }
    
    public NotificationTemplateType TemplateType { get; set; }
    
    public IList<NotificationHistory> Histories { get; set; }
}