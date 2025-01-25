namespace ModelArena.Models;

public record AzureAIModelConfig
{
    public required string Name { get; set; }
    public required string Endpoint { get; set; }
    public required string ApiKey { get; set; }
}