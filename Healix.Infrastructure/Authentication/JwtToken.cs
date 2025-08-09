using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class JwtToken : IJwtToken
{
    private readonly IConfiguration _configuration;

    public JwtToken(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(List<Claim> claims)
    {
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(Convert.ToInt16(_configuration["JWT:Expiration"])),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]!)),
                SecurityAlgorithms.HmacSha256
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
