using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/doctor-schedules")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DoctorScheduleController : ControllerBase
{
    private readonly IMediator _mediator;

    public DoctorScheduleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddDoctorSchedule([FromBody] AddDoctorScheduleCommand command)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        command.CurrentUserId = Guid.Parse(userId);

        var result = await _mediator.Send(command);
        return Ok(new { schedule = result });
    }

    [HttpPost]
    [Route("{scheduleId}")]
    public async Task<IActionResult> EditDoctorSchedule(
        [FromBody] EditDoctorScheduleCommand command,
        Guid scheduleId
    )
    {
        command.ScheduleId = scheduleId;
        var result = await _mediator.Send(command);
        return Ok(new { schedule = result });
    }

    [HttpGet]
    public async Task<IActionResult> GetDoctorSchedule([FromBody] GetDoctorScheduleQuery query)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        query.CurrentUserId = Guid.Parse(userId);

        var result = await _mediator.Send(query);
        return Ok(new { schedule = result });
    }
}
