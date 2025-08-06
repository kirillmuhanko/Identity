using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Company.Identity.Application.User.DTOs;

namespace Company.Identity.Api.User.Responses;

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

    [Required]
    [Description("JWT authentication token for the created user.")]
    [DefaultValue("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...")]
    public required string Token { get; set; }

    public static CreateUserResponse From(CreateUserDto dto)
    {
        return new CreateUserResponse
        {
            Id = dto.Id,
            UserName = dto.UserName,
            Email = dto.Email,
            Token = dto.Token
        };
    }
}