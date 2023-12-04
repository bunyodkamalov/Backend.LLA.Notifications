using System.Security.Cryptography.X509Certificates;
using Notifications.Domain.Enums;

namespace Notifications.Application.Common.Models.Querying;

public class NotificationTemplateFiler : FilterPagination
{
    public IList<NotificationType> TemplateType { get; set; }
}