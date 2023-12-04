using AutoMapper;
using Notifications.Application.Common.Identity.Services;
using Notifications.Application.Common.Notifications.Models;
using Notifications.Application.Common.Notifications.Services;
using Notifications.Domain.Common.Exeptions;
using Notifications.Domain.Entities;
using Notifications.Domain.Enums;
using Notifications.Domain.Extensions;

namespace Notifications.Infrastructure.Common.Notifications.Services;

public class EmailOrchestrationService : IEmailOrchestrationService
{
    private readonly IMapper _mapper;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IEmailRenderingService _renderingService;
    private readonly IEmailHistoryService _emailHistoryService;
    private readonly IUserService _userService;

    public EmailOrchestrationService(
        IMapper mapper,
        IEmailTemplateService emailTemplateService,
        IEmailSenderService emailSenderService,
        IEmailRenderingService renderingService,
        IEmailHistoryService emailHistoryService,
        IUserService userService
    )
    {
        _mapper = mapper;
        _emailTemplateService = emailTemplateService;
        _emailSenderService = emailSenderService;
        _renderingService = renderingService;
        _emailHistoryService = emailHistoryService;
        _userService = userService;
    }

    public async ValueTask<FuncResult<bool>> SendAsync(
        EmailNotificationRequest notificationRequest,
        CancellationToken cancellationToken = default)
    {
        var senderNotificationRequest = async () =>
        {
            var message = _mapper.Map<EmailMessage>(notificationRequest);

            var senderUser = (await _userService
                .GetByIdAsync(notificationRequest.SenderUserId!.Value, cancellationToken: cancellationToken))!;

            var receiverUser = (await _userService
                .GetByIdAsync(notificationRequest.ReceiverUserId, cancellationToken: cancellationToken))!;

            message.SenderEmailAddress = senderUser.EmailAddress;
            message.ReceiverEmailAddress = receiverUser.EmailAddress;

            message.Template =
                await _emailTemplateService.GetByTypeAsync(notificationRequest.TemplateType, true, cancellationToken) ??
                throw new InvalidOperationException(
                    $"Invalid template for sending {NotificationType.Email} notification");

            await _renderingService.RenderAsync(message, cancellationToken);

            await _emailSenderService.SendAsync(message, cancellationToken);

            var history = _mapper.Map<EmailHistory>(message);
            await _emailHistoryService.CreateAsync(history, cancellationToken: cancellationToken);

            return history.IsSuccessful;
        };

        return await senderNotificationRequest.GetValueAsync();
    }
}