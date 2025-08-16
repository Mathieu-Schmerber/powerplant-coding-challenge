namespace CodingChallenge.Models;

/// <summary>
/// Http error response.
/// </summary>
public record ErrorResponse
{
    public int StatusCode { get; init; }
    public string Message { get; init; } = string.Empty;
    public string? Detail { get; init; }
}