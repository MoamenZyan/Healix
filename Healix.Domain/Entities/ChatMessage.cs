namespace Healix.Domain.Entities
{
    public class ChatMessage : BaseEntity
    {
        public string Content { get; set; } = null!;
        public bool IsUser { get; set; }
        public Guid ChatId { get; set; }

        public virtual List<ChatMessageFile> Files { get; set; } = new List<ChatMessageFile>();
        public virtual ChatBot Chat { get; set; } = null!;

        public ChatMessageDto ToDto()
        {
            var dto = new ChatMessageDto()
            {
                Id = this.Id,
                ChatId = this.ChatId,
                CreatedAt = this.CreatedAt,
                Content = this.Content,
                IsUser = this.IsUser,
                Files = this.Files?.Select(x => x.ToDto()).ToList(),
            };

            return dto;
        }
    }

    public class ChatMessageDto : BaseEntity
    {
        public string Content { get; set; } = null!;
        public bool IsUser { get; set; }
        public Guid ChatId { get; set; }

        public virtual List<ChatMessageFileDto>? Files { get; set; } = null;
    }
}
