using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class OpenAIService
{
    private readonly string _apiKey;
    private readonly string _modelId;
    private readonly HttpClient _client;

    public OpenAIService(IConfiguration config, string modelId)
    {
        _apiKey = config["OpenAI:ApiKey"];
        _modelId = modelId;
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<string> GetCompletionAsync(string messages)
    {
        var request = new { model = _modelId, messages = messages };
        var json = JsonSerializer.Serialize(request);
        var response = await _client.PostAsync(
            $"https://api.openai.com/v1/chat/completions",
            new StringContent(json, Encoding.UTF8, "application/json"));
        return await response.Content.ReadAsStringAsync();
    }
}
