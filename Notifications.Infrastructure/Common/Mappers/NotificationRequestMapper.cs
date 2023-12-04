using AutoMapper;
using Notifications.Application.Common.Notifications.Models;

namespace Notifications.Infrastructure.Common.Mappers;

public class NotificationRequestMapper : Profile
{
    public NotificationRequestMapper()
    {
        CreateMap<NotificationRequest, EmailNotificationRequest>();
        CreateMap<NotificationRequest, SmsNotificationRequest>();
    }
}