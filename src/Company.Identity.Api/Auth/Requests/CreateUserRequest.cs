using System.ComponentModel.DataAnnotations;
using Company.Identity.Shared.Email.Attributes;

namespace Company.Identity.Api.Auth.Requests;

public class CreateUserRequest
{
    [Required(ErrorMessage = "User name is required.")]
    [MinLength(3, ErrorMessage = "User name must be at least 3 characters.")]
    public string UserName { get; set; } = null!;

    [EmailFormat(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; } = null!;
}