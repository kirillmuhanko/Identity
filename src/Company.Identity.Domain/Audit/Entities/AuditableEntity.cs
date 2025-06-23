namespace Company.Identity.Domain.Audit.Entities;

public class AuditableEntity
{
    public DateTime CreatedAt { get; protected set; }

    public string CreatedBy { get; protected set; } = null!;

    public DateTime? LastModifiedAt { get; protected set; }

    public string? LastModifiedBy { get; protected set; }
}