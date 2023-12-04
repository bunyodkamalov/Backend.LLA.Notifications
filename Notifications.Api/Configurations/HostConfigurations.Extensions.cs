using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Notifications.Api.Data;
using Notifications.Application.Common.Identity.Services;
using Notifications.Application.Common.Notifications.Brokers;
using Notifications.Application.Common.Notifications.Services;
using Notifications.Domain.Entities;
using Notifications.Infrastructure.Common.Identity.Services;
using Notifications.Infrastructure.Common.Notifications.Brokers;
using Notifications.Infrastructure.Common.Notifications.Services;
using Notifications.Infrastructure.Common.Settings;
using Notifications.Persistence.DataContext;
using Notifications.Persistence.Repositories;
using Notifications.Persistence.Repositories.Interfaces;

namespace WebApplication1.Configurations;

public static partial class HostConfigurations
{
    private static readonly ICollection<Assembly> Assemblies;
    
    static HostConfigurations()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblies(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserSettingsRepository, UserSettingsRepository>();

        builder.Services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserSettingsService, UserSettingsService>();
        
        return builder;
    }
    
    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services
            .Configure<TemplateRenderingSettings>(builder.Configuration.GetSection(nameof(TemplateRenderingSettings)))
            .Configure<NotificationSettings>(builder.Configuration.GetSection(nameof(NotificationSettings)))
            .Configure<SmtpEmailSenderSettings>(builder.Configuration.GetSection(nameof(SmtpEmailSenderSettings)))
            .Configure<TwilioSmsSenderSettings>(builder.Configuration.GetSection(nameof(TwilioSmsSenderSettings)));
        
        builder.Services.AddDbContext<NotificationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("NotificationsDatabaseConnection")));
        
        builder.Services
            .AddScoped<IEmailTemplateRepository, EmailTemplateRepository>()
            .AddScoped<IEmailHistoryRepository, EmailHistoryRepository>()
            .AddScoped<ISmsTemplateRepository, SmsTemplateRepository>()
            .AddScoped<ISmsHistoryRepository, SmsHistoryRepository>();

        builder.Services
            .AddScoped<IEmailSenderBroker, SmtpEmailSenderBroker>()
            .AddScoped<ISmsSenderBroker, TwilioSmsSenderBroker>();

        builder.Services
            .AddScoped<IEmailTemplateService, EmailTemplateService>()
            .AddScoped<ISmsTemplateService, SmsTemplateService>()
            .AddScoped<IEmailHistoryService, EmailHistoryService>()
            .AddScoped<ISmsHistoryService, SmsHistoryService>();

        builder.Services
            .AddScoped<IEmailSenderService, EmailSenderService>()
            .AddScoped<ISmsSenderService, SmsSenderService>()
            .AddScoped<IEmailRenderingService, EmailRenderingService>()
            .AddScoped<ISmsRenderingService, SmsRenderingService>();

        builder.Services
            .AddScoped<IEmailOrchestrationService, EmailOrchestrationService>()
            .AddScoped<ISmsOrchestrationService, SmsOrchestrationService>()
            .AddScoped<INotificationAggregatorService, NotificationAggregatorService>();
        
        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    private static async Task<WebApplication> SeedDataAsync(this WebApplication app)
    {
        await using var servicesScope = app.Services.CreateAsyncScope();
        await servicesScope.ServiceProvider.InitializeSeedAsync(servicesScope.ServiceProvider
            .GetRequiredService<IWebHostEnvironment>());
        
        return app;
    }
    
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}