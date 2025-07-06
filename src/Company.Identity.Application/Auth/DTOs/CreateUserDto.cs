namespace Company.Identity.Application.Auth.DTOs;

public class CreateUserDto
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;
}