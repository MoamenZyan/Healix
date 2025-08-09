using System.Threading.Tasks;
using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class SendMessageChatBotHandler : IRequestHandler<SendMessageChatBotCommand, ChatMessageDto>
{
    private readonly IGeminiService _geminiService;
    private readonly IS3Service _s3Service;
    private readonly IApplicationDbContext _context;

    public SendMessageChatBotHandler(
        IGeminiService geminiService,
        IS3Service s3Service,
        IApplicationDbContext context
    )
    {
        _geminiService = geminiService;
        _s3Service = s3Service;
        _context = context;
    }

    public async Task<ChatMessageDto> Handle(
        SendMessageChatBotCommand request,
        CancellationToken cancellationToken
    )
    {
        ChatBot? chat;
        List<Content> contents = new List<Content>();
        List<string> filesAsBase64 = new List<string>();

        if (request.ChatId == null)
        {
            chat = new ChatBot() { Id = Guid.NewGuid(), UserId = request.CurrentUserId };

            await _context.ChatBots.AddAsync(chat, cancellationToken);
        }
        else
        {
            chat = await _context.ChatBots.FirstOrDefaultAsync(x => x.Id == request.ChatId);
            if (chat == null)
            {
                throw new NotFoundException("chat not found to send message");
            }

            var messages = await _context
                .ChatMessages.Include(x => x.Files)
                .Where(x => x.ChatId == chat.Id)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();

            foreach (var message in messages)
            {
                contents.Add(await ConvertMessageToContent.ConvertMessage(message));
            }
        }

        var userMessage = new ChatMessage()
        {
            Id = Guid.NewGuid(),
            IsUser = true,
            ChatId = chat.Id,
            Content = request.Message,
        };

        await _context.ChatMessages.AddAsync(userMessage);

        if (request.Files != null)
        {
            List<ChatMessageFile> messageFiles = new List<ChatMessageFile>();

            foreach (var file in request.Files)
            {
                var url = await _s3Service.UploadFile(file);
                messageFiles.Add(
                    new ChatMessageFile()
                    {
                        Id = Guid.NewGuid(),
                        FileUrl = url,
                        ChatMessageId = userMessage.Id,
                        MimeType = file.ContentType,
                    }
                );

                filesAsBase64.Add(await ConvertFileToBase64.ConvertFile(url));
            }

            await _context.ChatMessageFiles.AddRangeAsync(messageFiles);

            userMessage.Files = messageFiles;
        }

        await _context.SaveChangesAsync(cancellationToken);

        contents.Add(await ConvertMessageToContent.ConvertMessage(userMessage));

        var result = await _geminiService.SendMessageAsync(contents);

        var chatBotMessage = new ChatMessage()
        {
            IsUser = false,
            Content = result.Parts.First().Text,
            ChatId = chat.Id,
        };

        await _context.ChatMessages.AddAsync(chatBotMessage, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return chatBotMessage.ToDto();
    }
}

public static class ConvertMessageToContent
{
    public static async Task<Content> ConvertMessage(ChatMessage message)
    {
        var role = message.IsUser switch
        {
            true => "user",
            false => "model",
        };

        List<Part> parts = new List<Part>();

        var textPart = new Part(text: message.Content);

        parts.Add(textPart);

        if (message.Files != null && message.Files.Count() > 0)
        {
            foreach (var file in message.Files)
            {
                parts.Add(
                    new Part(
                        inlineData: new InlineData(
                            file.MimeType,
                            await ConvertFileToBase64.ConvertFile(file.FileUrl)
                        )
                    )
                );
            }
        }

        return new Content(role, parts);
    }
}
