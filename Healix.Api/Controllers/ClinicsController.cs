using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/clinics")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ClinicsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClinicsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddClinic([FromForm] AddClinicCommand command)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        command.CurrentUserId = Guid.Parse(userId);
        var result = await _mediator.Send(command);

        return Ok(new { clinic = result });
    }

    [HttpGet]
    public async Task<IActionResult> GetClinics([FromQuery] GetClinicQuery query)
    {
        var result = await _mediator.Send(query);

        return Ok(new { clinics = result });
    }

    [HttpPut]
    public async Task<IActionResult> EditClinic([FromForm] EditClinicCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(new { clinic = result });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteClinics([FromQuery] DeleteClinicCommand command)
    {
        await _mediator.Send(command);

        return Ok("Clinic Deleted.");
    }
}
