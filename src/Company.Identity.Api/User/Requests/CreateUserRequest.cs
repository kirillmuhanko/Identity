using System.ComponentModel.DataAnnotations;

namespace Company.Identity.Api.User.Requests;

public class CreateUserRequest
{
    [Required(ErrorMessage = "UserName is required.")]
    [MinLength(3, ErrorMessage = "UserName must be at least 3 characters.")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = null!;
}