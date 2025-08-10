using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BudgetBeavers.Application.Utilities;

public static class Guard
{
    public static void AgainstNull<T>(T obj, [CallerArgumentExpression("obj")] string? paramName = null) where T : class?
    {
        if (obj == null)
            throw new ArgumentNullException(paramName);
    }

    public static void AgainstNullOrWhiteSpace(string? value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        paramName = ExtractParamName(paramName);
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{paramName} cannot be null or whitespace.", paramName);
    }

    public static void AgainstEmptyGuid(Guid id, [CallerArgumentExpression("id")] string? paramName = null)
    {
        paramName = ExtractParamName(paramName);
        if (id == Guid.Empty)
            throw new ArgumentException($"{paramName} cannot be empty.", paramName);
    }
    
    public static void AgainstKeyNotFound<T>([NotNull]T? obj, Guid id, [CallerArgumentExpression("id")] string? keyName = null) where T : class
    {
        if (obj == null)
            throw new KeyNotFoundException($"No entity found with the provided {keyName}: {id}.");
    }
    
    private static string? ExtractParamName(string? paramName)
    {
        if (string.IsNullOrEmpty(paramName))
            return paramName;

        var lastDotIndex = paramName.LastIndexOf('.');
        if (lastDotIndex >= 0 && lastDotIndex < paramName.Length - 1)
        {
            return paramName.Substring(lastDotIndex + 1);
        }
        return paramName;
    }
}