using AutoMapper;
using Notifications.Application.Common.Notifications.Models;

namespace Notifications.Infrastructure.Common.Mappers;

public class NotificationMessageMapper : Profile
{
    public NotificationMessageMapper()
    {
        CreateMap<NotificationMessage, EmailMessage>();
        CreateMap<NotificationMessage, SmsMessage>();
    }
}