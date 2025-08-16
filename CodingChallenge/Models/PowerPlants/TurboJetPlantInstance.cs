namespace CodingChallenge.Models.PowerPlants;

/// <summary>
/// Turbo jet power plant instance.
/// <remarks>Uses kerosine as fuel.</remarks>
/// <see cref="PowerPlantInstanceBase"/> 
/// </summary>
public sealed record TurboJetPlantInstance : PowerPlantInstanceBase
{
    public TurboJetPlantInstance(PowerPlantDefinition definition, Fuels fuels)
        : base(definition, fuels.KerosineCost) { }
}