namespace CodingChallenge.Configurations;

/// <summary>
/// Power plant configuration.
/// </summary>
public class PowerPlantConfig : Dictionary<string, PowerPlantEntry>
{
    /// <summary>
    /// Section name within configuration.
    /// </summary>
    public const string SECTION_NAME = "PowerPlants";
}

/// <summary>
/// Power plant entry.
/// </summary>
public record PowerPlantEntry
{
    /// <summary>
    /// Power plant type.
    /// </summary>
    public string? Type { get; init; } = string.Empty;
}