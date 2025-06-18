namespace Company.Identity.Domain.Common.Entities;

public class AuditableEntity
{
    public DateTime CreatedAt { get; protected set; }

    public string CreatedBy { get; protected set; } = "system";

    public DateTime? LastModifiedAt { get; protected set; }

    public string? LastModifiedBy { get; protected set; }
}