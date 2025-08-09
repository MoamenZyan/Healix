using System.Text.Json;
using Healix.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

public class GeminiService : IGeminiService
{
    private readonly HttpClient _httpClient;
    private readonly GeminiConfigurations _configurations;

    public GeminiService(IOptions<GeminiConfigurations> configurations)
    {
        _configurations = configurations.Value;

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_configurations.BaseUrl);
    }

    public async Task<Content> SendMessageAsync(List<Content> contents)
    {
        return await SendMessageToChatbotAsync(contents, ConfigurePrompt);
    }

    public async Task<Content> GetPatientSummary(List<Content> contents)
    {
        return await SendMessageToChatbotAsync(contents, ConfigurePatientPrompt);
    }

    public async Task<Content> GetFamilySummary(List<Content> contents)
    {
        return await SendMessageToChatbotAsync(contents, ConfigureFamilyPrompt);
    }

    private async Task<Content> SendMessageToChatbotAsync(
        List<Content> contents,
        Func<Task<string>> func
    )
    {
        var parts = new List<Part>();

        var promptPart = new Part(text: await func());

        parts.Add(promptPart);

        var newContent = new Content(role: "user", parts: parts);

        contents.Insert(0, newContent);

        var request = new SendMessageRequest(contents);

        // Serialize the request object to JSON
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(request),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        // Add the API key as a query parameter
        var fullEndpoint = $"{_configurations.Endpoint}?key={_configurations.ApiKey}";

        // Send the POST request
        var response = await _httpClient.PostAsync(fullEndpoint, jsonContent);

        // Handle the response
        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SendMessageResponse>(responseBody)!;

            return result.Candidates.First().Content;
        }
        else
        {
            throw new BadRequestException(
                $"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}"
            );
        }
    }

    private async Task<string> ConfigurePrompt()
    {
        return await File.ReadAllTextAsync("../Healix.Infrastructure/Services/Chatbot/prompt.txt");
    }

    private async Task<string> ConfigurePatientPrompt()
    {
        return await File.ReadAllTextAsync(
            "../Healix.Infrastructure/Services/Chatbot/patientSummaryPrompt.txt"
        );
    }

    private async Task<string> ConfigureFamilyPrompt()
    {
        return await File.ReadAllTextAsync(
            "../Healix.Infrastructure/Services/Chatbot/FamilySummaryPrompt.txt"
        );
    }
}
