using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/appointments")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddAppointment([FromBody] AddAppointmentCommand command)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        command.CurrentUserId = Guid.Parse(userId);

        var result = await _mediator.Send(command);
        return Ok(new { appointment = result });
    }

    [HttpGet]
    [Route("doctor-appointments")]
    public async Task<IActionResult> GetDoctorAppointment()
    {
        var query = new GetDoctorAppointmentsQuery();

        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

        query.CurrentUserId = Guid.Parse(userId);

        var result = await _mediator.Send(query);

        return Ok(new { appointments = result });
    }

    [HttpPut]
    public async Task<IActionResult> SetAppointmentStatus(
        [FromBody] SetAppointmentStatusCommand command
    )
    {
        var result = await _mediator.Send(command);

        return Ok(new { appointment = result });
    }
}
