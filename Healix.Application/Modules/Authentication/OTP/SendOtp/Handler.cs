using Hangfire;
using MediatR;

public class SendOtpHandler : IRequestHandler<SendOtpCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IEmailContext _emailContext;
    private readonly IBackgroundJobClient _backgroundClient;

    public SendOtpHandler(
        IApplicationDbContext context,
        IEmailContext emailContext,
        IBackgroundJobClient backgroundClient
    )
    {
        _context = context;
        _emailContext = emailContext;
        _backgroundClient = backgroundClient;
    }

    public async Task<Guid> Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        var otp = new Otp() { Id = Guid.NewGuid() };

        await _context.Otps.AddAsync(otp, cancellationToken);

        var emailSender = _emailContext.GetEmailStrategy(EmailType.OTP);

        var obj = new Dictionary<string, string> { { "Email", request.Email } };

        _backgroundClient.Enqueue(() => emailSender!.Send(obj));

        await _context.SaveChangesAsync(cancellationToken);

        return otp.Id;
    }
}
