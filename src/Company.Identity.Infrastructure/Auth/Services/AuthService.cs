using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Company.Identity.Application.Auth.Interfaces.Services;
using Company.Identity.Domain.User.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Company.Identity.Infrastructure.Auth.Services;

public class AuthService(IConfiguration config) : IAuthService
{
    private readonly PasswordHasher<UserEntity> _hasher = new();

    public string HashPassword(UserEntity user, string password)
    {
        var hashed = _hasher.HashPassword(user, password);
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
        var secretKey = config["Jwt:Key"];
        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var tokenDescriptor = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(tokenDescriptor);
        return token;
    }
}