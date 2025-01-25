namespace ModelArena;

public class ModelSessionServiceFactory
{
    private readonly AzureAIModelManager _modelManager;

    // ReSharper disable once ConvertToPrimaryConstructor
    public ModelSessionServiceFactory(AzureAIModelManager modelManager)
    {
        _modelManager = modelManager;
    }

    public ModelSessionService Create(string modelName)
    {
        // Resolve the model configuration by name
        var modelConfig = _modelManager.GetModelConfig(modelName);
        return new ModelSessionService(modelConfig);
    }

    public IEnumerable<string> GetAvailableModelNames()
    {
        // Return all available model names
        return _modelManager.GetModelNames();
    }
}