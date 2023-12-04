using Notifications.Application.Common.Notifications.Models;

namespace Notifications.Application.Common.Notifications.Services;

public interface IEmailRenderingService
{
    ValueTask<string> RenderAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}