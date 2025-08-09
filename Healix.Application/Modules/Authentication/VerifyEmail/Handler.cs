using System.Text.RegularExpressions;
using Healix.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand>
{
    private readonly IApplicationDbContext _context;

    public VerifyEmailHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        Regex validateEmailRegex = new Regex(
            "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"
        );

        if (!validateEmailRegex.IsMatch(request.Email))
            throw new BadRequestException("not valid email.");

        var emailExist = await _context.Users.AnyAsync(x => x.Email == request.Email);
        if (emailExist)
            throw new ConflictException("email already exists.");
    }
}
