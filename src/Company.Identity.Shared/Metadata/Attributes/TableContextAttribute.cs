namespace Company.Identity.Shared.Metadata.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class TableContextAttribute(params string[] tableNames) : Attribute
{
    public IReadOnlyList<string> TableNames { get; } = tableNames;
}