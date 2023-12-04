using System.Collections.Immutable;

namespace WebApplication1.Configurations;

public static partial class HostConfigurations
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddMappers()
            .AddValidators()
            .AddIdentityInfrastructure()
            .AddNotificationInfrastructure()
            .AddExposers()
            .AddDevTools();
        return new (builder);
    }

    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        await app.SeedDataAsync();
        app.UseExposers().UseDevTools();
        
        return app;
    }
}