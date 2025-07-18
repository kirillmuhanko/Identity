using System.Reflection;
using Company.Identity.Shared.Metadata.Attributes;

namespace Company.Identity.Shared.Metadata.Providers;

public static class MetadataProvider
{
    public static string GetDisplayName<TEntity>()
    {
        var type = typeof(TEntity);
        var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
        var displayName = attribute?.Name;
        var fallbackName = type.Name;

        var result = !string.IsNullOrWhiteSpace(displayName)
            ? displayName
            : fallbackName;

        return result;
    }
}