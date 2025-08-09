using MediatR;

public class VerifyOtpCommand : IRequest
{
    public string Secret { get; set; } = null!;
    public Guid OtpId { get; set; }
}
