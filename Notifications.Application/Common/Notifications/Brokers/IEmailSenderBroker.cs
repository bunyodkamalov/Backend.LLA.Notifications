using Notifications.Application.Common.Notifications.Models;

namespace Notifications.Application.Common.Notifications.Brokers;

public interface IEmailSenderBroker
{
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}