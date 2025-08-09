using Healix.Application.Modules;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> CreateUser([FromForm] UserSignupCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { user = result.Item1, token = result.Item2 });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { token = result.Item1, user = result.Item2 });
    }

    [HttpPost]
    [Route("send-otp")]
    public async Task<IActionResult> SendOtp([FromBody] SendOtpCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { id = result });
    }

    [HttpPost]
    [Route("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpCommand command)
    {
        await _mediator.Send(command);
        return Ok("otp verified!");
    }

    [HttpPost]
    [Route("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailCommand command)
    {
        await _mediator.Send(command);
        return Ok("email verified");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { user = result });
    }

    [HttpGet]
    [Route("doctors")]
    public async Task<IActionResult> GetDoctors([FromBody] GetDoctorQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(new { doctors = result });
    }
}
