using System.ComponentModel.DataAnnotations;
using Company.Identity.Application.Auth.Commands;

namespace Company.Identity.Api.Auth.Requests;

public record CreateUserRequest(
    [property: Required(ErrorMessage = "User name is required.")]
    [property: MinLength(3, ErrorMessage = "User name must be at least 3 characters.")]
    string UserName,

    [property: EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    string Email,

    [property: Required(ErrorMessage = "Password is required.")]
    [property: MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    string Password
)
{
    public CreateUserCommand ToCommand() =>
        new(UserName, Email, Password);
}