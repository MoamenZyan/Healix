using System.Security.Claims;

public interface IJwtToken
{
    string GenerateJwtToken(List<Claim> claims);
}
