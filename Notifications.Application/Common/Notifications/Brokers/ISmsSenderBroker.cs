using Notifications.Application.Common.Notifications.Models;

namespace Notifications.Application.Common.Notifications.Brokers;

public interface ISmsSenderBroker
{
    ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default);
}