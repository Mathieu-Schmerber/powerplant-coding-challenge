namespace CodingChallenge.Models.PowerPlants;

public sealed record TurboJetPlantInstance : PowerPlantInstance
{
    public TurboJetPlantInstance(PowerPlantDefinition definition, Fuels fuels)
        : base(definition, fuels.Kerosine) { }
}