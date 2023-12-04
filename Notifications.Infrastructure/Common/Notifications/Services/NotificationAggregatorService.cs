using AutoMapper;
using Microsoft.Extensions.Options;
using Notifications.Application.Common.Identity.Services;
using Notifications.Application.Common.Models.Querying;
using Notifications.Application.Common.Notifications.Models;
using Notifications.Application.Common.Notifications.Services;
using Notifications.Domain.Common.Exeptions;
using Notifications.Domain.Entities;
using Notifications.Domain.Enums;
using Notifications.Domain.Extensions;
using Notifications.Infrastructure.Common.Settings;

namespace Notifications.Infrastructure.Common.Notifications.Services;

public class NotificationAggregatorService : INotificationAggregatorService
{
    private readonly IMapper _mapper;
    private readonly ISmsTemplateService _smsTemplateService;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly ISmsOrchestrationService _smsOrchestrationService;
    private readonly IEmailOrchestrationService _emailOrchestrationService;
    private readonly IUserSettingsService _userSettingsService;
    private readonly IOptions<NotificationSettings> _notificationSettings;
    private readonly IUserService _userService;

    public NotificationAggregatorService(
        IMapper mapper,
        IOptions<NotificationSettings> notificationSettings,
        ISmsTemplateService smsTemplateService,
        IEmailTemplateService emailTemplateService,
        ISmsOrchestrationService smsOrchestrationService,
        IEmailOrchestrationService emailOrchestrationService,
        IUserSettingsService userSettingsService,
        IUserService userService
    )
    {
        _mapper = mapper;
        _smsTemplateService = smsTemplateService;
        _emailTemplateService = emailTemplateService;
        _smsOrchestrationService = smsOrchestrationService;
        _emailOrchestrationService = emailOrchestrationService;
        _userSettingsService = userSettingsService;
        _notificationSettings = notificationSettings;
        _userService = userService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(
        NotificationRequest notificationRequest,
        CancellationToken cancellationToken = default
    )
    {
        var sendNotificationTask = async () =>
        {
            var senderUser = notificationRequest.SenderUserId.HasValue
                ? await _userService.GetByIdAsync(notificationRequest.SenderUserId.Value,
                    cancellationToken: cancellationToken)
                : await _userService.GetSystemUserAsync(true, cancellationToken);

            notificationRequest.SenderUserId = senderUser!.Id;

            var receiverUser = await _userService.GetByIdAsync(notificationRequest.ReceiverUserId,
                cancellationToken: cancellationToken);

            if (!notificationRequest.Type.HasValue && receiverUser!.UserSettings.PreferredNotificationType.HasValue)
                notificationRequest.Type = receiverUser!.UserSettings.PreferredNotificationType!.Value;

            if (!notificationRequest.Type.HasValue)
                notificationRequest.Type = _notificationSettings.Value.DefaultNotificationType;

            var sendNotificationTask = notificationRequest.Type switch
            {
                NotificationType.Sms => _smsOrchestrationService.SendAsync(
                    _mapper.Map<SmsNotificationRequest>(notificationRequest),
                    cancellationToken
                ),
                NotificationType.Email => _emailOrchestrationService.SendAsync(
                    _mapper.Map<EmailNotificationRequest>(notificationRequest),
                    cancellationToken),
                _ => throw new NotImplementedException("This type of notification is not supported yet.")
            };

            var result = await sendNotificationTask;
            return result.Data;
        };

        return await sendNotificationTask.GetValueAsync();
    }

    public async ValueTask<IList<NotificationTemplate>> GetTemplatesByFilter(NotificationTemplateFiler filter,
        CancellationToken asNoTracking,
        CancellationToken cancellationToken = default)
    {
        var templates = new List<NotificationTemplate>();
        
        if(filter.TemplateType.Contains(NotificationType.Sms))
            templates.AddRange(
                await _smsTemplateService.GetByFilterAsync(filter, cancellationToken: cancellationToken));
        
        if (filter.TemplateType.Contains(NotificationType.Email))
            templates.AddRange(
                await _emailTemplateService.GetByFilterAsync(filter, cancellationToken: cancellationToken));

        return templates;
    }
}