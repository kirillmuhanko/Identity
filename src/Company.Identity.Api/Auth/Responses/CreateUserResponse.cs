using Company.Identity.Application.Auth.DTOs;

namespace Company.Identity.Api.Auth.Responses;

public record CreateUserResponse(Guid Id, string UserName, string Email)
{
    public static CreateUserResponse FromDto(CreateUserDto dto) =>
        new(dto.Id, dto.UserName, dto.Email);
}