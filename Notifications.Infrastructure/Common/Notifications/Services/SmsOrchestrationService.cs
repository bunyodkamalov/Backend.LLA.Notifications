using AutoMapper;
using Notifications.Application.Common.Identity.Services;
using Notifications.Application.Common.Notifications.Models;
using Notifications.Application.Common.Notifications.Services;
using Notifications.Domain.Common.Exeptions;
using Notifications.Domain.Entities;
using Notifications.Domain.Enums;
using Notifications.Domain.Extensions;
using Notifications.Persistence.DataContext;

namespace Notifications.Infrastructure.Common.Notifications.Services;

public class SmsOrchestrationService : ISmsOrchestrationService
{
    private readonly IMapper _mapper;
    private readonly ISmsSenderService _smsSenderService;
    private readonly ISmsRenderingService _smsRenderingService;
    private readonly ISmsHistoryService _smsHistoryService;
    private readonly ISmsTemplateService _smsTemplateService;
    private readonly IUserService _userService;
    private readonly NotificationDbContext _dbContext;

    public SmsOrchestrationService(
        IMapper mapper,
        ISmsSenderService smsSenderService,
        ISmsRenderingService smsRenderingService,
        ISmsHistoryService smsHistoryService,
        ISmsTemplateService smsTemplateService,
        IUserService userService,
        NotificationDbContext dbContext
        )
    {
        _mapper = mapper;
        _smsSenderService = smsSenderService;
        _smsRenderingService = smsRenderingService;
        _smsHistoryService = smsHistoryService;
        _smsTemplateService = smsTemplateService;
        _userService = userService;
        _dbContext = dbContext;
    }
    
    
    public async ValueTask<FuncResult<bool>> SendAsync(SmsNotificationRequest notificationRequest, CancellationToken cancellationToken = default)
    {
        var sendNotificationRequest = async () =>
        {
            var message = _mapper.Map<SmsMessage>(notificationRequest);

            var senderUser = (await _userService.GetByIdAsync(notificationRequest.SenderUserId!.Value,
                cancellationToken: cancellationToken))!;
            
            var receiverUser =
                (await _userService.GetByIdAsync(notificationRequest.ReceiverUserId, cancellationToken: cancellationToken))!;

            message.SenderPhoneNumber = senderUser.PhoneNumber;
            message.ReceiverPhoneNumber = receiverUser.PhoneNumber;

            // get template
            message.Template =
                await _smsTemplateService.GetByTypeAsync(notificationRequest.TemplateType, true, cancellationToken) ??
                throw new InvalidOperationException(
                    $"Invalid template for sending {NotificationType.Sms} notification");

            // blogs.Comments.Add(new Comment { Title = "My comment" });

            // render template
            await _smsRenderingService.RenderAsync(message, cancellationToken);

            // send message
            await _smsSenderService.SendAsync(message, cancellationToken);

            // save history

            var history = _mapper.Map<SmsHistory>(message);
            var test = _dbContext.Entry(history.Template).State;

            await _smsHistoryService.CreateAsync(history, cancellationToken: cancellationToken);

            return history.IsSuccessful;
        };

        return await sendNotificationRequest.GetValueAsync();
    }
}