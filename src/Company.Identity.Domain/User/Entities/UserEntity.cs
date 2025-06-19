using Company.Identity.Domain.Common.Entities;

namespace Company.Identity.Domain.User.Entities;

public class UserEntity : AuditableEntity
{
    // ReSharper disable once UnusedMember.Global
    public UserEntity()
    {
    }

    public UserEntity(Guid id, string userName, string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
        IsActive = true;
    }

    public Guid Id { get; private set; }

    public string UserName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public void Deactivate()
    {
        IsActive = false;
    }
}