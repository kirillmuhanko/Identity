using Company.Identity.Domain.Common.Entities;

namespace Company.Identity.Domain.User.Entities;

public class UserEntity : AuditableEntity
{
    public UserEntity(Guid id, string userName, string email)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id must not be empty.", nameof(id));

        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("UserName is required.", nameof(userName));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        if (!email.Contains('@'))
            throw new ArgumentException("Email must be valid.", nameof(email));

        Id = id;
        UserName = userName;
        Email = email;
        IsActive = true;
    }

    public Guid Id { get; private set; }

    public string UserName { get; private set; }

    public string Email { get; private set; }

    public bool IsActive { get; private set; }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("User is already inactive.");

        IsActive = false;
    }
}