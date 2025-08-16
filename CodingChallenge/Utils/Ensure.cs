namespace CodingChallenge.Utils;

/// <summary>
/// Utility class used for public contract validations.
/// </summary>
public static class Ensure
{
    /// <summary>
    /// Ensures that an object is not null.
    /// </summary>
    /// <param name="instance">The instance to validate.</param>
    /// <param name="name">The field name.</param>
    /// <typeparam name="T">The instance type.</typeparam>
    /// <exception cref="ArgumentNullException">The instance is null.</exception>
    public static void NotNull<T>(T instance, string? name = null)
    {
        if (instance == null)
            throw new ArgumentNullException($"{name ?? typeof(T).Name} cannot be null.");
    }
}