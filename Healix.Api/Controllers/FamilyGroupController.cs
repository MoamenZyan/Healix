using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/family-groups")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FamilyGroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public FamilyGroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddFamilyGroup([FromBody] AddFamilyGroupCommand command)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        command.CurrentUserId = Guid.Parse(userId);
        var result = await _mediator.Send(command);

        return Ok(new { familyGroup = result });
    }

    [HttpPost]
    [Route("{id}")]
    public async Task<IActionResult> AddUserToFamilyGroup(Guid id)
    {
        var command = new AddUserToFamilyGroupCommand();

        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

        command.GroupId = id;
        command.CurrentUserId = Guid.Parse(userId);

        await _mediator.Send(command);

        return Ok("user added to family group successfully!");
    }

    [HttpGet]
    public async Task<IActionResult> GetFamilyGroup([FromBody] GetFamilyGroupQuery query)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        query.CurrentUserId = Guid.Parse(userId);
        var result = await _mediator.Send(query);

        return Ok(new { familyGroup = result });
    }
}
