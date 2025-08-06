namespace Company.Identity.Application.User.Commands;

public record CreateUserCommand(
    string UserName,
    string Email,
    string Password
);