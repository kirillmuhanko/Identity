using Company.Identity.Domain.Common.Entities;
using Company.Identity.Shared.Email.Validators;

namespace Company.Identity.Domain.User.Entities;

public class UserEntity : AuditableEntity
{
    // ReSharper disable once UnusedMember.Global
    public UserEntity()
    {
    }

    public UserEntity(string userName, string email, string passwordHash)
    {
        SetUserName(userName);
        SetEmail(email);
        SetPasswordHash(passwordHash);
    }

    public string UserName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    public void SetUserName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");
        UserName = name.Trim();
    }

    public void SetEmail(string email)
    {
        if (!EmailValidator.IsEmailFormatValid(email))
            throw new ArgumentException("Invalid email format.");

        Email = email.Trim().ToLower();
    }

    public void SetPasswordHash(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password hash cannot be empty.");

        if (newPasswordHash == PasswordHash)
            throw new InvalidOperationException("New password must be different from the current password.");

        PasswordHash = newPasswordHash;
    }
}