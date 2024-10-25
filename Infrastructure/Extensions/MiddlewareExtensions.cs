using Scheduler.Infrastructure.Middlewares;

namespace Scheduler.Infrastructure.Extensions;

public static class MiddlewareExtensions
{
    private const string devHmacKey = "SecretKey:HmacSecretKey";
    private const string stagingHmacKey = "STAGING_HmacSecretKey";
    private const string prodHmacKey = "PROD_HmacSecretKey";

    public static IApplicationBuilder AddHmacAuthentication(this IApplicationBuilder builder)
    {
        var configuration = builder.ApplicationServices.GetRequiredService<IConfiguration>();
        var environment = builder.ApplicationServices.GetRequiredService<IHostEnvironment>();
#pragma warning disable CA2208
        var hmacKey = GetHmacKey(configuration, environment)
            ?? throw new ArgumentNullException("HmacKey", "HmacKey must be configured");

        return builder.UseMiddleware<HmacAuthenticationMiddleware>(hmacKey);
    }

    private static string? GetHmacKey(IConfiguration configuration, IHostEnvironment environment)
    {
        return environment.EnvironmentName switch
        {
            "Development" => configuration[devHmacKey],
            "Staging" => configuration[stagingHmacKey],
            "Production" => configuration[prodHmacKey],
            _ => throw new InvalidOperationException("Unrecognized environment.")
        };
    }
}
