using CodingChallenge.Interfaces;

namespace CodingChallenge.Models.PowerPlants;

public abstract record PowerPlantInstance : IPowerPlantInstance
{
    public string Name { get; }
    public float FuelCost { get; protected init; }
    public float CostPerUnit { get; protected init; }
    public float Efficiency { get; protected init; }
    public float MinOutput { get; protected init; }
    public float MaxOutput { get; protected init; }

    protected PowerPlantInstance(PowerPlantDefinition definition, float fuelCost)
    {
        Name = definition.Name;
        Efficiency = definition.Efficiency;
        FuelCost = fuelCost;
        
        CostPerUnit = GetCostPerUnit(fuelCost);
        MinOutput = Round1(definition.Pmin);
        MaxOutput = Round1(definition.Pmax);
    }
    
    protected static float Round1(float value) =>
        MathF.Round(value, 1, MidpointRounding.AwayFromZero);

    private float GetCostPerUnit(float fuelCost) => Round1(1 * fuelCost / Efficiency);
}