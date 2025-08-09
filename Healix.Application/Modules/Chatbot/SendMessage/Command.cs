using Healix.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

public class SendMessageChatBotCommand : IRequest<ChatMessageDto>
{
    public Guid? ChatId { get; set; }
    public string Message { get; set; } = null!;
    public List<IFormFile>? Files { get; set; }
    public Guid CurrentUserId { get; set; }
}
