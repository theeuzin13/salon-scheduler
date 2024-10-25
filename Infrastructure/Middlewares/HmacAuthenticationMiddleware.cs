using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Application.Services.Interfaces;

namespace Scheduler.Infrastructure.Middlewares;

public class HmacAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private const string HmacHeaderName = "X-HMAC";
    private const string ApiKeyHeaderName = "X-API-KEY";
    private readonly string _hmacKey;

    public HmacAuthenticationMiddleware(string hmacKey, RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _hmacKey = hmacKey ?? throw new ArgumentNullException(nameof(hmacKey));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.RouteValues;

        if (!context.Request.Headers.TryGetValue(HmacHeaderName, out var hmacValue) ||
            !context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyValue))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Missing HMAC or Api Key header");
            return;
        }

        var apiKeyService = context.RequestServices.GetService<IApiKeyService>();

        var providedHmac = context.Request.Headers[HmacHeaderName].FirstOrDefault();
        var apiKeyHeader = context.Request.Headers[ApiKeyHeaderName].FirstOrDefault();

        var apiKeyEntity = await apiKeyService!.GetByTokenAsync(apiKeyHeader!);

        if (apiKeyEntity == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }

        if (!string.Equals(apiKeyEntity.Token, apiKeyHeader, StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key does not match");
            return;
        }

        if (!IsRequestValid(context, providedHmac!, apiKeyHeader!))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid HMAC signature");
            return;
        }

        await _next(context);
    }

    private bool IsRequestValid(HttpContext context, string providedHmac, string apiKeyHeader)
    {
        context.Request.EnableBuffering();

        using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true);
        var body = reader.ReadToEndAsync().Result;

        context.Request.Body.Position = 0;

        var method = context.Request.Method;
        var path = context.Request.Path;

        var secretKeyBytes = Encoding.UTF8.GetBytes(_hmacKey);
        using var hmac = new HMACSHA512(secretKeyBytes);
        var dataToSign = string.IsNullOrEmpty(body)
            ? $"{method}:{path}:{apiKeyHeader}"
            : $"{method}:{path}:{apiKeyHeader}:{body}";
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
        var computedHmac = BitConverter.ToString(computedHash).Replace("-", "").ToLower();

        return providedHmac.Equals(computedHmac, StringComparison.OrdinalIgnoreCase);
    }
}
