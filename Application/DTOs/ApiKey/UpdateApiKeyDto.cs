namespace Scheduler.Application.DTOs.ApiKey;

public class UpdateApiKeyDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Token { get; set; }
}
