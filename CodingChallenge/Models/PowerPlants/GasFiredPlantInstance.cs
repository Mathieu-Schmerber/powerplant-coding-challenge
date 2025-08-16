namespace CodingChallenge.Models.PowerPlants;

public sealed record GasFiredPlantInstance : PowerPlantInstance
{
    public GasFiredPlantInstance(PowerPlantDefinition definition, Fuels fuels)
        : base(definition, fuels.Gas) { }
}