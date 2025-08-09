using System.Text.Json.Serialization;

namespace Healix.Domain.Entities
{
    public class ChatBot : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

        public ChatbotDto ToDto()
        {
            var dto = new ChatbotDto()
            {
                Id = this.Id,
                CreatedAt = this.CreatedAt,
                LastMessage = this
                    .Messages.OrderBy(x => x.CreatedAt)
                    .Select(x => x.ToDto())
                    .LastOrDefault(),

                Messages = this.Messages?.OrderBy(x => x.CreatedAt).Select(x => x.ToDto()).ToList(),
            };

            return dto;
        }

        public ChatbotDto ToMinimalDto()
        {
            var dto = new ChatbotDto()
            {
                Id = this.Id,
                CreatedAt = this.CreatedAt,
                LastMessage = this
                    .Messages.OrderBy(x => x.CreatedAt)
                    .Select(x => x.ToDto())
                    .LastOrDefault(),

                Messages = null,
            };

            return dto;
        }
    }

    public class ChatbotDto : BaseEntity
    {
        public ChatMessageDto? LastMessage { get; set; } = null!;
        public virtual List<ChatMessageDto>? Messages { get; set; } = null;
    }
}

// API Models
public class SendMessageRequest
{
    [JsonPropertyName("contents")]
    public List<Content> Contents { get; set; }

    [JsonPropertyName("generationConfig")]
    private GenerationConfig GenerationConfig { get; set; }

    public SendMessageRequest(List<Content> contents)
    {
        Contents = contents;
        GenerationConfig = new GenerationConfig(0.7, 0.9, 50);
    }
}

public class Content
{
    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("parts")]
    public List<Part> Parts { get; set; }

    public Content(string role, List<Part> parts)
    {
        Role = role;
        Parts = parts;
    }
}

public class Part
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("inlineData")]
    public InlineData InlineData { get; set; }

    public Part(string? text = null, InlineData? inlineData = null)
    {
        Text = text!;
        InlineData = inlineData!;
    }
}

public class InlineData
{
    [JsonPropertyName("mimeType")]
    public string MimeType { get; set; }

    [JsonPropertyName("data")]
    public string Data { get; set; }

    public InlineData(string mimeType, string data)
    {
        MimeType = mimeType;
        Data = data;
    }
}

public class GenerationConfig
{
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("topP")]
    public double TopP { get; set; }

    [JsonPropertyName("maxOutputTokens")]
    public int MaxOutputTokens { get; set; }

    public GenerationConfig(double temperature, double topP, int maxOutputTokens)
    {
        Temperature = temperature;
        TopP = topP;
        MaxOutputTokens = maxOutputTokens;
    }
}

public class SendMessageResponse
{
    [JsonPropertyName("candidates")]
    public List<Candidate> Candidates { get; set; }

    public SendMessageResponse(List<Candidate> candidates)
    {
        Candidates = candidates;
    }
}

public class Candidate
{
    [JsonPropertyName("content")]
    public Content Content { get; set; }

    public Candidate(Content content)
    {
        Content = content;
    }
}
