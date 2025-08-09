using MediatR;

public class VerifyEmailCommand : IRequest
{
    public string Email { get; set; } = null!;
}
