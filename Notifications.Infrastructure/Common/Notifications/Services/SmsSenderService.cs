using Notifications.Application.Common.Notifications.Brokers;
using Notifications.Application.Common.Notifications.Models;
using Notifications.Application.Common.Notifications.Services;
using Notifications.Domain.Extensions;

namespace Notifications.Infrastructure.Common.Notifications.Services;

public class SmsSenderService : ISmsSenderService
{
    private readonly IEnumerable<ISmsSenderBroker> _smsSenderBrokers;

    public SmsSenderService(IEnumerable<ISmsSenderBroker> smsSenderBrokers)
    {
        _smsSenderBrokers = smsSenderBrokers;
    }
    
    public async ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        foreach (var smsSenderBroker in _smsSenderBrokers)
        {
            var sendNotificationTask = () => smsSenderBroker.SendAsync(smsMessage, cancellationToken);
            var result = await sendNotificationTask.GetValueAsync();

            smsMessage.IsSuccess = result.IsSuccess;
            smsMessage.ErrorMessage = result.Exception!.Message;
            return result.IsSuccess;
        }

        return false;
    }
}