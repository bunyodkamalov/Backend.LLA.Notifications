using Notifications.Application.Common.Notifications.Models;
using Notifications.Domain.Common.Exeptions;

namespace Notifications.Application.Common.Notifications.Services;

public interface IEmailOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(EmailNotificationRequest notificationRequest,
        CancellationToken cancellationToken = default);
}