namespace Company.Identity.Application.User.Events;

public class UserCreatedEvent(string email, string userName)
{
    public string Email { get; } = email;

    public string UserName { get; } = userName;
}