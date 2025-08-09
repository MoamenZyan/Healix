using MediatR;

public class SendOtpCommand : IRequest<Guid>
{
    public string Email { get; set; } = null!;
}
