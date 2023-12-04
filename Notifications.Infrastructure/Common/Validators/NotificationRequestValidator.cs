using FluentValidation;
using Notifications.Application.Common.Identity.Services;
using Notifications.Application.Common.Notifications.Models;
using Notifications.Domain.Enums;
using Notifications.Infrastructure.Common.Identity.Services;

namespace Notifications.Infrastructure.Common.Validators;

public class NotificationRequestValidator : AbstractValidator<NotificationRequest>
{
    public NotificationRequestValidator(IUserService userService)
    {
        var templateRequireSender = new List<NotificationTemplateType>()
        {
            NotificationTemplateType.ReferralNotification
        };

        RuleFor(temp => temp.SenderUserId)
            .NotEqual(Guid.Empty)
            .NotNull()
            .When(request => templateRequireSender.Contains(request.TemplateType))
            .CustomAsync(async (senderUserId, context, cancellationToken) =>
            {
                var user = await userService.GetByIdAsync(senderUserId!.Value, true, cancellationToken);
                
                if(user is null)
                    context.AddFailure("Sender user not found");
            });

        RuleFor(request => request.ReceiverUserId)
            .NotEqual(Guid.Empty)
            .CustomAsync(async (receiverUserId, context, cancellationToken) =>
            {
                var user = await userService.GetByIdAsync(receiverUserId, true, cancellationToken);
                
                if(user is null)
                    context.AddFailure("Receiver user not found");
            });
    }
}