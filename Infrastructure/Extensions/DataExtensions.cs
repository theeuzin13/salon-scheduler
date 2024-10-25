using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Interfaces;
using Scheduler.Infrastructure.Repositories;

namespace Scheduler.Infrastructure.Extensions;

public static class DataExtensions
{
    private const string devConnectionString = "DEV_ConnectionString";
    private const string stagingConnectionString = "STAGING_ConnectionString";
    private const string productionConnectionString = "PRODUCTION_ConnectionString";
    private static readonly string[] tags = ["db", "postgresql"];
    private static readonly string[] tagsArray = ["api"];

    public static IServiceCollection AddEntityFramework(this IServiceCollection services, WebApplicationBuilder builder)
    {
        string? connectionString = GetConnectionString(builder.Configuration, builder.Environment);

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string is null or empty.");
        }

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IApiKeyRepository, ApiKeyRepository>();


        return services;
    }

    private static string? GetConnectionString(IConfiguration configuration, IHostEnvironment environment)
    {
        return environment.EnvironmentName switch
        {
            "Development" => configuration[devConnectionString],
            "Staging" => Environment.GetEnvironmentVariable(stagingConnectionString),
            "Production" => Environment.GetEnvironmentVariable(productionConnectionString),
            _ => throw new InvalidOperationException("Unrecognized environment.")
        };
    }
}
