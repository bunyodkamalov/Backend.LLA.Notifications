using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;
using Notifications.Domain.Enums;
using Notifications.Persistence.DataContext;

namespace Notifications.Api.Data;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedAsync(
        this IServiceProvider serviceProvider,
        IWebHostEnvironment webHostEnvironment
    )
    {
        var notificationDbContext = serviceProvider.GetRequiredService<NotificationDbContext>();

        if (!await notificationDbContext.EmailTemplates.AnyAsync())
            await notificationDbContext.SeedEmailTemplates(webHostEnvironment);

        if (!await notificationDbContext.SmsTemplates.AnyAsync())
            await notificationDbContext.SeedSmsTemplates();

        if (!await notificationDbContext.Users.AnyAsync())
            await notificationDbContext.SeedUsersAsync();

        if (!await notificationDbContext.UserSettings.AnyAsync())
            await notificationDbContext.SeedUserSettingsAsync();

        if (notificationDbContext.ChangeTracker.HasChanges())
            await notificationDbContext.SaveChangesAsync();
    }

    private static async ValueTask SeedEmailTemplates(
        this NotificationDbContext notificationDbContext,
        IWebHostEnvironment webHostEnvironment
    )
    {
        var emailTemplateTypes = new List<NotificationTemplateType>
        {
            NotificationTemplateType.WelcomeNotification,
            NotificationTemplateType.EmailAddressVerificationNotification,
            NotificationTemplateType.ReferralNotification
        };

        var emailTemplateContents = await Task.WhenAll(emailTemplateTypes.Select(async templateType =>
        {
            var filePath = Path.Combine(webHostEnvironment.ContentRootPath,
                "Data",
                "EmailTemplates",
                Path.ChangeExtension(templateType.ToString(), "html"));
            return (TemplateType: templateType, TemplateContent: await File.ReadAllTextAsync(filePath));
        }));

        var emailTemplates = emailTemplateContents.Select(templateContent => templateContent.TemplateType switch
        {
            NotificationTemplateType.WelcomeNotification => new EmailTemplate
            {
                TemplateType = templateContent.TemplateType,
                Subject = "Welcome to our service!",
                Content = templateContent.TemplateContent
            },
            NotificationTemplateType.EmailAddressVerificationNotification => new EmailTemplate
            {
                TemplateType = templateContent.TemplateType,
                Subject = "Confirm your email address",
                Content = templateContent.TemplateContent
            },
            NotificationTemplateType.ReferralNotification => new EmailTemplate
            {
                TemplateType = templateContent.TemplateType,
                Subject = "You have been referred!",
                Content = templateContent.TemplateContent
            },
            _ => throw new NotSupportedException("Template type not supported.")
        });

        await notificationDbContext.EmailTemplates.AddRangeAsync(emailTemplates);
    }

    private static async ValueTask SeedSmsTemplates(this NotificationDbContext notificationDbContext)
    {
        await notificationDbContext.SmsTemplates.AddRangeAsync(new SmsTemplate
            {
                TemplateType = NotificationTemplateType.WelcomeNotification,
                Content =
                    "Welcome {{UserName}}! We're thrilled to have you on board. Get ready to explore and enjoy our services"
            },
            new SmsTemplate
            {
                TemplateType = NotificationTemplateType.PhoneNumberVerificationNotification,
                Content =
                    "Hey {{UserName}}. To secure your account, please verify your phone number using this link: {{PhoneNumberVerificationLink}}"
            },
            new SmsTemplate
            {
                TemplateType = NotificationTemplateType.ReferralNotification,
                Content =
                    "You've been invited to join by a friend {{SenderName}}! Sign up today and enjoy exclusive benefits. Use referral code"
            });
    }

    private static async ValueTask SeedUsersAsync(this NotificationDbContext notificationDbContext)
    {
        await notificationDbContext.Users.AddRangeAsync(new User
            {
                UserName = "System",
                PhoneNumber = "+12132931337",
                EmailAddress = "sultonbek.rakhimov.recovery@gmail.com",
                Role = RoleType.System
            },
            new User
            {
                Id = Guid.Parse("6c0021b5-818c-4f4c-b622-97f73fab473e"),
                UserName = "John",
                PhoneNumber = "+998999663258",
                EmailAddress = "sultonbek.rakhimov@gmail.com",
            },
            new User
            {
                Id = Guid.Parse("12c7e7df-4484-4181-bf96-d340e229c16b"),
                UserName = "Jane",
                PhoneNumber = "+12132931338",
                EmailAddress = "jane.doe@gmail.com",
            });
    }

    private static async ValueTask SeedUserSettingsAsync(this NotificationDbContext notificationDbContext)
    {
        await notificationDbContext.UserSettings.AddRangeAsync(new UserSettings
            {
                Id = Guid.Parse("6c0021b5-818c-4f4c-b622-97f73fab473e"),
                PreferredNotificationType = NotificationType.Sms
            },
            new UserSettings
            {
                Id = Guid.Parse("12c7e7df-4484-4181-bf96-d340e229c16b"),
                PreferredNotificationType = NotificationType.Sms
            });
    }
}