using Microsoft.Extensions.Options;
using Notifications.Application.Common.Notifications.Brokers;
using Notifications.Application.Common.Notifications.Models;
using Notifications.Infrastructure.Common.Settings;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Notifications.Infrastructure.Common.Notifications.Brokers;

public class TwilioSmsSenderBroker : ISmsSenderBroker
{
    private readonly TwilioSmsSenderSettings _twilioSmsSenderSettings;

    public TwilioSmsSenderBroker(IOptions<TwilioSmsSenderSettings> twilioSmsSenderSettings)
    {
        _twilioSmsSenderSettings = twilioSmsSenderSettings.Value;
    }
    
    public ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        TwilioClient.Init(_twilioSmsSenderSettings.AccountSid, _twilioSmsSenderSettings.AuthToken);

        var messageContent = MessageResource.Create(
            body: smsMessage.Message,
            from: new Twilio.Types.PhoneNumber(_twilioSmsSenderSettings.SenderPhoneNumber),
            to: new Twilio.Types.PhoneNumber(smsMessage.ReceiverPhoneNumber)
        );

        return new ValueTask<bool>(true);
    }
}