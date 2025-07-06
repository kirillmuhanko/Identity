namespace Company.Identity.Api.Auth.Requests;

public class CreateUserRequest
{
    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}