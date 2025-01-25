using Microsoft.Extensions.Options;
using ModelArena.Models;

namespace ModelArena;

public class AzureAIModelManager
{
    private readonly List<AzureAIModelConfig> _models;

    public AzureAIModelManager(IOptions<List<AzureAIModelConfig>> config)
    {
        _models = config.Value;
    }

    public AzureAIModelConfig GetModelConfig(string modelName)
    {
        return _models.FirstOrDefault(m => m.Name.Equals(modelName, StringComparison.OrdinalIgnoreCase))
               ?? throw new Exception($"Model '{modelName}' not found in configuration.");
    }

    public ModelSessionService CreateModelSession(string modelName)
    {
        var modelConfig = GetModelConfig(modelName);
        return new ModelSessionService(modelConfig);
    }

    public IList<string> GetModelNames() => _models.Select(m => m.Name).ToArray();
}