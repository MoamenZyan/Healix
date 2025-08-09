using Healix.Domain.Entities;
using MediatR;

public class GetChatsQuery : IRequest<List<ChatbotDto>>
{
    public Guid? ChatId { get; set; }
    public Guid? CurrentUserId { get; set; }
    public bool IsMinimal { get; set; }
}
