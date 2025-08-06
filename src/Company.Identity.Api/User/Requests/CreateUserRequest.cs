using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Company.Identity.Application.User.Commands;

namespace Company.Identity.Api.User.Requests;

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
    [MinLength(8,
        ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and symbol.")]
    [DefaultValue("Str0ngP@ss!")]
    [Description(
        "Secure password for the user account. Must be at least 8 characters and include uppercase, lowercase, number, and symbol.")]
    public required string Password { get; set; }

    public CreateUserCommand ToCommand()
    {
        return new CreateUserCommand(UserName, Email, Password);
    }
}