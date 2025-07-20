using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Company.Identity.Application.Auth.Commands;

namespace Company.Identity.Api.Auth.Requests;

public class CreateUserRequest
{
    [Required(ErrorMessage = "User name is required.")]
    [MinLength(3, ErrorMessage = "User name must be at least 3 characters.")]
    [DefaultValue("johndoe")]
    [Description("Unique username for the account.")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [DefaultValue("johndoe@example.com")]
    [Description("Email address of the user.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    [DefaultValue("P@ssw0rd!")]
    [Description("Secure password for the user account.")]
    public required string Password { get; set; }

    public CreateUserCommand ToCommand()
    {
        return new CreateUserCommand(UserName, Email, Password);
    }
}