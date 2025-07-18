namespace Company.Identity.Shared.Metadata.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class DisplayNameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}