namespace CodingChallenge.Configurations;

public class PowerPlantConfig : Dictionary<string, PowerPlantEntry>
{
    public const string SECTION_NAME = "PowerPlants";
}

public record PowerPlantEntry
{
    public string Type { get; set; } = string.Empty;
}

