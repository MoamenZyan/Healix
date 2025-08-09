using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/patient-records")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PatientRecordsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientRecordsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> UploadPatientRecord([FromForm] AddRecordCommand command)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        command.PatientId = Guid.Parse(userId);

        var result = await _mediator.Send(command);

        return Ok(new { record = result });
    }

    [HttpGet]
    public async Task<IActionResult> GetPatientRecords([FromQuery] GetRecordsQuery query)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        query.PatientId = Guid.Parse(userId);

        var result = await _mediator.Send(query);

        return Ok(new { records = result });
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePatientRecords([FromQuery] DeleteRecordCommand query)
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        query.CurrentUserId = Guid.Parse(userId);

        await _mediator.Send(query);

        return Ok("History Record Deleted.");
    }
}
