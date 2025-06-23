namespace Company.Identity.Application.User.Commands;

public class CreateUserCommand
{
    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;
}