using Healix.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand>
{
    private readonly IApplicationDbContext _context;

    public VerifyOtpHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var otp = await _context.Otps.FirstOrDefaultAsync(
            x => x.Id == request.OtpId,
            cancellationToken
        );

        if (otp == null)
            throw new NotFoundException("otp not found");

        if (otp.Secret != request.Secret)
            throw new UnauthorizedException("secret not correct!");
    }
}
