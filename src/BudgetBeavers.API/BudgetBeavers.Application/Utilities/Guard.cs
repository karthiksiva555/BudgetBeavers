using System.Diagnostics.CodeAnalysis;

namespace BudgetBeavers.Application.Utilities;

public static class Guard
{
    public static void AgainstNull<T>(T obj, string paramName) where T : class
    {
        if (obj == null)
            throw new ArgumentNullException(paramName);
    }

    public static void AgainstNullOrWhiteSpace(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{paramName} cannot be null or whitespace.", paramName);
    }

    public static void AgainstEmptyGuid(Guid id, string paramName)
    {
        if (id == Guid.Empty)
            throw new ArgumentException($"{paramName} cannot be empty.", paramName);
    }
    
    public static void AgainstKeyNotFound<T>([NotNull]T? obj, Guid id, string keyName) where T : class
    {
        if (obj == null)
            throw new KeyNotFoundException($"No entity found with the provided {keyName}: {id}.");
    }
}