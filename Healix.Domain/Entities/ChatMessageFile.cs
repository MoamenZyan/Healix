using Healix.Domain.Entities;

public class ChatMessageFile : BaseEntity
{
    public string FileUrl { get; set; } = null!;
    public string MimeType { get; set; } = null!;

    public Guid ChatMessageId { get; set; }
    public virtual ChatMessage ChatMessage { get; set; } = null!;

    public ChatMessageFileDto ToDto()
    {
        var dto = new ChatMessageFileDto()
        {
            Id = this.Id,
            CreatedAt = this.CreatedAt,
            FileUrl = this.FileUrl,
        };

        return dto;
    }
}

public class ChatMessageFileDto : BaseEntity
{
    public string FileUrl { get; set; } = null!;
}
