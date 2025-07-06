using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Company.Identity.Application.Auth.Interfaces.Services;
using Company.Identity.Domain.User.Entities;
using Company.Identity.Infrastructure.Auth.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Company.Identity.Infrastructure.Auth.Services;

public class AuthService(IOptions<JwtOptions> jwtOptions) : IAuthService
{
    private readonly PasswordHasher<UserEntity> _hasher = new();
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public string HashPassword(string password)
    {
        var userEntity = new UserEntity();
        var hashed = _hasher.HashPassword(userEntity, password);
        return hashed;
    }

    public bool VerifyPassword(UserEntity user, string password)
    {
        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        var isValid = result == PasswordVerificationResult.Success;
        return isValid;
    }

    public string GenerateJwtToken(UserEntity user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var tokenDescriptor = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(tokenDescriptor);
        return token;
    }
}