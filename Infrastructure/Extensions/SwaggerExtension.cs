using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Scheduler.Infrastructure.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSwaggerGen(c =>
        {
            if (builder.Environment.IsProduction())
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Scheduler",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Matheus Silva",
                        Email = "matt.henrique13700@gmail.com"
                    }
                });
            }
            else if (builder.Environment.IsStaging())
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Scheduler Homologacao",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Matheus Silva",
                        Email = "matt.henrique13700@gmail.com"
                    }
                });
            }
            else if (builder.Environment.IsDevelopment())
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Scheduler Localhost",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Matheus Silva",
                        Email = "matt.henrique13700@gmail.com"
                    }
                });
            }

            c.OperationFilter<AddHmacHeaderParameter>();
            c.OperationFilter<AddApiKeyHeaderParameter>();
        });

        return services;

    }

    private class AddHmacHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-HMAC",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }
    }

    private class AddApiKeyHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-API-KEY",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }
    }
}
