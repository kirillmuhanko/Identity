namespace Company.Identity.Shared.Metadata.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public sealed class TableContextAttribute(string tableName) : Attribute
{
    public string TableName { get; } = tableName;
}