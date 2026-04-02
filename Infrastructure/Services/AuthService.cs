using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AuthService : IAuthService
{
    private readonly JWTConfig _jwtConfig;

    public AuthService(
        IOptions<JWTConfig> jwtConfig,
        IUserRepository userRepository)
    {
        _jwtConfig = jwtConfig.Value;
    }
    public AuthResponse GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecurityKey));
        var claims = new List<Claim>
        {
            //new Claim(type:"Id", user.Id.ToString()),
            new Claim(type:ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        var accessToken = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            expires: DateTime.UtcNow.AddDays(7),
            claims: claims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        string refreshToken = Convert.ToBase64String(randomNumber);
        DateTime refreshTokenExpiration = DateTime.UtcNow.AddDays(1);

        var response = new AuthResponse()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(accessToken),
            ExpiresOn = accessToken.ValidTo
        };
        return response;
    }

}