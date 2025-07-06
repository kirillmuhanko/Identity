using Company.Identity.Domain.Common.Entities;

namespace Company.Identity.Domain.User.Entities;

public class UserEntity : AuditableEntity
{
    // ReSharper disable once UnusedMember.Global
    public UserEntity()
    {
    }

    public UserEntity( string userName, string email)
    {
        UserName = userName;
        Email = email;
        IsActive = true;
        CreatedAt = DateTime.UtcNow; // temp
        CreatedBy = "System"; // temp
    }

    public string UserName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password hash cannot be empty.");

        if (newPasswordHash == PasswordHash)
            throw new InvalidOperationException("New password must be different from the current password.");

        PasswordHash = newPasswordHash;
    }
}