namespace Company.Identity.Application.User.DTOs;

public class CreateUserDto
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;
}