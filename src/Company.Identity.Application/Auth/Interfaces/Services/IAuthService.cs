using Company.Identity.Domain.User.Entities;

namespace Company.Identity.Application.Auth.Interfaces.Services;

public interface IAuthService
{
    string HashPassword(UserEntity user, string password);

    bool VerifyPassword(UserEntity user, string password);

    string GenerateJwtToken(UserEntity user);
}