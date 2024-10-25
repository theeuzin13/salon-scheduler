namespace Scheduler.Application.DTOs.ApiKey;

public class CreateApiKeyDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Token { get; set; }
}
