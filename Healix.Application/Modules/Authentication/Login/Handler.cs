using System.Security.Claims;
using Healix.Application.Exceptions;
using Healix.Domain.Entities;
using Healix.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Healix.Application.Modules;

public class UserLoginHandler : IRequestHandler<UserLoginCommand, (string, ApplicationUserWrapper)>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtToken _token;

    public UserLoginHandler(IApplicationDbContext context, IJwtToken token)
    {
        _context = context;
        _token = token;
    }

    public async Task<(string, ApplicationUserWrapper)> Handle(
        UserLoginCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _context.Users.FirstOrDefaultAsync(
            x => x.Email == request.Email,
            cancellationToken
        );

        if (user == null)
            throw new UnauthorizedException("email / password incorrect");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            throw new UnauthorizedException("email / password incorrect");

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!),
            new Claim(ClaimTypes.Role, user.UserType.ToString()),
        };

        var token = _token.GenerateJwtToken(claims);

        var wrapper = new ApplicationUserWrapper(null, user);

        return (token, wrapper);
    }
}
