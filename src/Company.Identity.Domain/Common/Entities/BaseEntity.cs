namespace Company.Identity.Domain.Common.Entities;

public abstract class BaseEntity
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public Guid Id { get; protected set;}
}