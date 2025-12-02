using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

public static class JwtUtils
{
    public static string GenerateToken(int customerId, IConfiguration configuration)
    {
        var key = configuration["Jwt:Key"] ?? throw new Exception("JWT key is missing in configuration.");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, customerId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // gives each token a unique identifier
        };

        var issuer = configuration["Jwt:Issuer"] ?? throw new Exception("JWT 'Issuer' is missing in configuration.");
        var audience = configuration["Jwt:Audience"] ?? throw new Exception("JWT 'Audience' is missing in configuration.");
        var expiresIn = configuration["Jwt:ExpiresIn"] ?? throw new Exception("JWT 'ExpiresIn' is missing in configuration.");

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(expiresIn)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
