using Notifications.Application.Common.Notifications.Models;
using Notifications.Domain.Common.Exeptions;

namespace Notifications.Application.Common.Notifications.Services;

public interface ISmsOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(SmsNotificationRequest notificationRequest,
        CancellationToken cancellationToken = default);
}