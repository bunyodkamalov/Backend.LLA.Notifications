using Notifications.Application.Common.Notifications.Models;

namespace Notifications.Application.Common.Notifications.Services;

public interface ISmsSenderService
{
    ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default);
}