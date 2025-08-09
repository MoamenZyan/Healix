using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/chatbot")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ChatbotController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatbotController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromForm] SendMessageChatBotCommand command)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        command.CurrentUserId = Guid.Parse(userId);

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetChats([FromQuery] GetChatsQuery query)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        query.CurrentUserId = Guid.Parse(userId);

        var result = await _mediator.Send(query);

        return Ok(new { chats = result });
    }

    [HttpPost]
    [Route("patient-summary")]
    public async Task<IActionResult> PatientSummary([FromBody] PatientSummaryCommand command)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        command.CurrentUserId = Guid.Parse(userId);

        var result = await _mediator.Send(command);

        return Ok(new { summary = result });
    }

    [HttpPost]
    [Route("family-summary")]
    public async Task<IActionResult> FamilySummary([FromBody] FamilySummaryCommand command)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        command.CurrentUserId = Guid.Parse(userId);

        var result = await _mediator.Send(command);

        return Ok(new { summary = result });
    }
}
