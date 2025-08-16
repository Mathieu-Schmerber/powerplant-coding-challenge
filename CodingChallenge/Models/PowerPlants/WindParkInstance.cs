namespace CodingChallenge.Models.PowerPlants;

/// <summary>
/// Wind power plant instance.
/// <remarks>Its maximum output is based on the wind percentage.</remarks>
/// <remarks>Its fuel cost is considered 0.</remarks>
/// <see cref="PowerPlantInstanceBase"/>
/// </summary>
public sealed record WindParkInstance : PowerPlantInstanceBase
{
    public WindParkInstance(PowerPlantDefinition definition, Fuels fuels) : base(definition, fuelCost: 0)
    {
        MaxOutput = Round1(definition.Pmax * (fuels.WindPercentage / 100f));
        
        // The wind park is either on or off.
        // When on, the output is fixed.
        MinOutput = MaxOutput;
    }
}