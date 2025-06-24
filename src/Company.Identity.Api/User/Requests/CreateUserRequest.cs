namespace Company.Identity.Api.User.Requests;

public class CreateUserRequest
{
    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;
}