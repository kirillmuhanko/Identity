using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Company.Identity.Application.Auth.DTOs;

namespace Company.Identity.Api.Auth.Responses;

public class CreateUserResponse
{
    [Required]
    [Description("Unique identifier of the created user.")]
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid Id { get; set; }

    [Required]
    [Description("Username of the created user.")]
    [DefaultValue("johndoe")]
    public required string UserName { get; set; }

    [Required]
    [EmailAddress]
    [Description("Email address of the created user.")]
    [DefaultValue("johndoe@example.com")]
    public required string Email { get; set; }

    public static CreateUserResponse FromDto(CreateUserDto dto)
    {
        return new CreateUserResponse
        {
            Id = dto.Id,
            UserName = dto.UserName,
            Email = dto.Email
        };
    }
}