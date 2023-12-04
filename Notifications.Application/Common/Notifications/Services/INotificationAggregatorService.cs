using Notifications.Application.Common.Models.Querying;
using Notifications.Application.Common.Notifications.Models;
using Notifications.Domain.Common.Exeptions;
using Notifications.Domain.Entities;

namespace Notifications.Application.Common.Notifications.Services;

public interface INotificationAggregatorService
{
    ValueTask<FuncResult<bool>> SendAsync(NotificationRequest notificationRequest,
        CancellationToken cancellationToken = default);

    ValueTask<IList<NotificationTemplate>> GetTemplatesByFilter(NotificationTemplateFiler filter,
        CancellationToken asNoTracking,
        CancellationToken cancellationToken = default);
}