using FluentValidation;
using Notifications.Domain.Entities;
using Notifications.Domain.Enums;

namespace Notifications.Infrastructure.Common.Validators;

public class EmailTemplateValidator : AbstractValidator<EmailTemplate>
{
    public EmailTemplateValidator()
    {
        RuleFor(temp => temp.Content)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(256);

        RuleFor(temp => temp.Type)
            .Equal(NotificationType.Email);
    }
}