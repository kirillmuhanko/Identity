using Company.Identity.Domain.Audit.Entities;

namespace Company.Identity.Domain.User.Entities;

public class UserEntity : AuditableEntity
{
    // ReSharper disable once UnusedMember.Global
    public UserEntity()
    {
    }

    public UserEntity(string userName, string email)
    {
        UserName = userName;
        Email = email;
        IsActive = true;
        CreatedAt = DateTime.UtcNow; // temp
        CreatedBy = "System"; // temp
    }

    // ReSharper disable once UnassignedGetOnlyAutoProperty
    public Guid Id { get; set; }

    public string UserName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public void Deactivate()
    {
        IsActive = false;
    }
}