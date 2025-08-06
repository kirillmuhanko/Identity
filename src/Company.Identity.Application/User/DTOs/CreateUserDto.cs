using Company.Identity.Domain.User.Entities;

namespace Company.Identity.Application.User.DTOs;

public record CreateUserDto(Guid Id, string UserName, string Email, string Token)
{
    public static CreateUserDto From(UserEntity user, string token) =>
        new(user.Id, user.UserName, user.Email, token);
}