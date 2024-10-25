using Scheduler.Application.Services.Interfaces;
using Scheduler.Application.Services;

namespace Scheduler.Infrastructure.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IApiKeyService, ApiKeyService>();
        return services;
    }
}
