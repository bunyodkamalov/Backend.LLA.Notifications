using Notifications.Application.Common.Notifications.Models;

namespace Notifications.Application.Common.Notifications.Services;

public interface ISmsRenderingService
{
    ValueTask<string> RenderAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default);
}