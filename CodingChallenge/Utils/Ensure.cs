namespace CodingChallenge.Utils;

public static class Ensure
{
    public static void NotNull<T>(T instance, string? name = null)
    {
        if (instance == null)
            throw new ArgumentNullException($"{name ?? typeof(T).Name} cannot be null.");
    }
}