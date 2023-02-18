using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;

public class OpenAIService
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public OpenAIService(string apiKey)
    {
        var _handler = new HttpClientHandler();
        _handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
        _apiKey = apiKey;
        _httpClient = new HttpClient(_handler);
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<string> GetCompletionAsync(string prompt)
    {
        var requestBody = new
        {
            prompt = prompt,
            temperature = 0.5,
            max_tokens = 50,
            top_p = 1,
            frequency_penalty = 0,
            presence_penalty = 0
        };
        
        _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/engines/davinci-codex/completions");
        var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress,requestBody);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        var jsonResponse = JsonDocument.Parse(responseContent);

        return jsonResponse.RootElement.GetProperty("choices")[0].GetProperty("text").GetString();
    }
}
