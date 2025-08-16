namespace CodingChallenge.Models.PowerPlants;

public sealed record WindParkInstance : PowerPlantInstance
{
    public WindParkInstance(PowerPlantDefinition definition, Fuels fuels) : base(definition, fuelCost: 0)
    {
        MinOutput = Round1(definition.Pmax * (fuels.Wind / 100f));
        MaxOutput = MinOutput;
    }
}