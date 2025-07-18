namespace Company.Identity.Shared.Entity.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class EntityDisplayNameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}