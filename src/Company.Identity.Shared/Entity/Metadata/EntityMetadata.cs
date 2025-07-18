using System.Reflection;
using Company.Identity.Shared.Entity.Attributes;

namespace Company.Identity.Shared.Entity.Metadata;

public static class EntityMetadata
{
    public static string GetDisplayName<TEntity>()
    {
        var type = typeof(TEntity);
        var attribute = type.GetCustomAttribute<EntityDisplayNameAttribute>();
        var displayName = attribute?.Name;
        var fallbackName = type.Name;

        var result = !string.IsNullOrWhiteSpace(displayName)
            ? displayName
            : fallbackName;

        return result;
    }
}