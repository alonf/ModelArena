using Azure;
using Azure.AI.Inference;
using ModelArena.Models;

namespace ModelArena;

public class ModelSessionService
{
    private readonly ChatCompletionsClient _client;
    private readonly List<ChatRequestMessage> _messageHistory = new();

    public ModelSessionService(AzureAIModelConfig modelConfig)
    {
        var endpoint = new Uri(modelConfig.Endpoint);
        var credential = new AzureKeyCredential(modelConfig.ApiKey);
        _client = new ChatCompletionsClient(endpoint, credential, new AzureAIInferenceClientOptions());
    }

    public void SetSystemPrompt(string systemPrompt)
    {
        systemPrompt += Environment.NewLine +
                        "Use markdown in your answers. You can use md tables and code blocks when applicable.";

        // Remove the existing system message if present
        if (_messageHistory.FirstOrDefault() is ChatRequestSystemMessage)
        {
            _messageHistory.RemoveAt(0);
        }

        // Add the new system message at the start of the history
        _messageHistory.Insert(0, new ChatRequestSystemMessage(systemPrompt));
    }


    public void AddUserMessage(string userMessage)
    {
        _messageHistory.Add(new ChatRequestUserMessage(userMessage));
    }

    public void ClearHistory()
    {
        _messageHistory.Clear();
    }

    public async IAsyncEnumerable<string> GetStreamingResponseAsync()
    {
        var options = new ChatCompletionsOptions
        {
            Messages = _messageHistory
        };

        var response = await _client.CompleteStreamingAsync(options);

        await foreach (var update in response)
        {
            if (!string.IsNullOrEmpty(update.ContentUpdate))
            {
                yield return update.ContentUpdate;
            }
        }
    }

    public IEnumerable<ChatRequestMessage> GetHistory()
    {
        return _messageHistory;
    }
}