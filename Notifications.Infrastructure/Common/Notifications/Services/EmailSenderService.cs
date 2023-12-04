using Notifications.Application.Common.Notifications.Brokers;
using Notifications.Application.Common.Notifications.Models;
using Notifications.Application.Common.Notifications.Services;
using Notifications.Domain.Extensions;
using Notifications.Infrastructure.Common.Notifications.Brokers;

namespace Notifications.Infrastructure.Common.Notifications.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly IEnumerable<IEmailSenderBroker> _emailSenderBrokers;

    public EmailSenderService(IEnumerable<IEmailSenderBroker> emailSenderBrokers)
    {
        _emailSenderBrokers = emailSenderBrokers;
    }
    
    public async ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        foreach (var emailSenderBroker in _emailSenderBrokers)
        {
            var sendNotificationTask = () => emailSenderBroker.SendAsync(emailMessage, cancellationToken);
            var result = await sendNotificationTask.GetValueAsync();

            emailMessage.IsSuccess = result.IsSuccess;
            emailMessage.ErrorMessage = result.Exception?.Message;
            return result.IsSuccess;
        }

        return false;
    }
}