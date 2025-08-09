using Healix.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetChatsHandler : IRequestHandler<GetChatsQuery, List<ChatbotDto>>
{
    private readonly IApplicationDbContext _context;

    public GetChatsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ChatbotDto>> Handle(
        GetChatsQuery request,
        CancellationToken cancellationToken
    )
    {
        List<ChatbotDto> chatbots;
        if (request.ChatId != null)
        {
            var chats = await _context
                .ChatBots.Include(x => x.Messages)
                .ThenInclude(x => x.Files)
                .Where(x => x.Id == request.ChatId && x.UserId == request.CurrentUserId)
                .ToListAsync(cancellationToken);

            chatbots = request.IsMinimal switch
            {
                true => chats.Select(x => x.ToMinimalDto()).ToList(),
                false => chats.Select(x => x.ToDto()).ToList(),
            };
        }
        else
        {
            var chats = await _context
                .ChatBots.Include(x => x.Messages)
                .ThenInclude(x => x.Files)
                .Where(x => x.UserId == request.CurrentUserId)
                .ToListAsync(cancellationToken);

            chatbots = request.IsMinimal switch
            {
                true => chats.Select(x => x.ToMinimalDto()).ToList(),
                false => chats.Select(x => x.ToDto()).ToList(),
            };
        }

        return chatbots;
    }
}
