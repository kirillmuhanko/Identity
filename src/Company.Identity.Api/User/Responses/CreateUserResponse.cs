namespace Company.Identity.Api.User.Responses;

public class CreateUserResponse
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;
}