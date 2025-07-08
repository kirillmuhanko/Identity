using Company.Identity.Domain.User.Entities;

namespace Company.Identity.Application.Auth.DTOs;

public record CreateUserDto(Guid Id, string UserName, string Email)
{
    public static CreateUserDto FromEntity(UserEntity user) =>
        new(user.Id, user.UserName, user.Email);
}