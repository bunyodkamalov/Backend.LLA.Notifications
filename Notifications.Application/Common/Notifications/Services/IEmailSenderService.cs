using Notifications.Application.Common.Notifications.Models;
using Notifications.Domain.Entities;

namespace Notifications.Application.Common.Notifications.Services;

public interface IEmailSenderService
{
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}