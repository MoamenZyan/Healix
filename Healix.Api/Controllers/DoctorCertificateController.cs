using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/doctor-certificates")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DoctorCertificatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DoctorCertificatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddDoctorCertificate(
        [FromForm] AddDoctorCertificateCommand command
    )
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        command.CurrentUserId = Guid.Parse(userId);

        await _mediator.Send(command);
        return Ok("certificate uploaded!");
    }
}
