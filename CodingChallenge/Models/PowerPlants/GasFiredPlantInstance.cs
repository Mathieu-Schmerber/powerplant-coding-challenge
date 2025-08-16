namespace CodingChallenge.Models.PowerPlants;

/// <summary>
/// Gas fired power plant instance.
/// <see cref="PowerPlantInstanceBase"/>
/// </summary>
public sealed record GasFiredPlantInstance : PowerPlantInstanceBase
{
    public GasFiredPlantInstance(PowerPlantDefinition definition, Fuels fuels)
        : base(definition, fuels.GasCost) { }
}