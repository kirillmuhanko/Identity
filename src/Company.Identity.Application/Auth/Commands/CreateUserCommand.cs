namespace Company.Identity.Application.Auth.Commands;

public record CreateUserCommand(
    string UserName,
    string Email,
    string Password
);