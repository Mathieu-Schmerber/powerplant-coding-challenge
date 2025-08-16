using CodingChallenge.Interfaces;

namespace CodingChallenge.Models.PowerPlants;

/// <summary>
/// Power plant instance base class.
/// </summary>
public abstract record PowerPlantInstanceBase : IPowerPlantInstance
{
    /// <inheritdoc/>
    public string Name { get; }
    
    /// <inheritdoc/>
    public float CostPerMWh { get; protected init; }
    
    /// <inheritdoc/>
    public float MinOutput { get; protected init; }
    
    /// <inheritdoc/>
    public float MaxOutput { get; protected init; }
    
    /// <summary>
    /// The fuel cost.
    /// </summary>
    protected float FuelCost { get; init; }
    
    /// <summary>
    /// The power plant efficiency.
    /// </summary>
    protected float Efficiency { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PowerPlantInstanceBase"/> class.
    /// </summary>
    /// <param name="definition">The power plant definition.</param>
    /// <param name="fuelCost">The fuel cost.</param>
    protected PowerPlantInstanceBase(PowerPlantDefinition definition, float fuelCost)
    {
        Name = definition.Name;
        Efficiency = definition.Efficiency;
        FuelCost = fuelCost;
        
        CostPerMWh = GetCostPerUnit(fuelCost);
        MinOutput = Round1(definition.Pmin);
        MaxOutput = Round1(definition.Pmax);
    }
    
    /// <summary>
    /// Utility method to round a float to 1 decimal place.
    /// </summary>
    /// <param name="value">The values to round.</param>
    /// <returns>Te rounded value.</returns>
    protected static float Round1(float value) =>
        MathF.Round(value, 1, MidpointRounding.AwayFromZero);

    /// <summary>
    /// Calculates the cost of producing one unit of electricity based on the fuel cost and efficiency.
    /// </summary>
    /// <param name="fuelCost">The fuel cost.</param>
    /// <returns>The cost per unit.</returns>
    private float GetCostPerUnit(float fuelCost) => Round1(1 * fuelCost / Efficiency);
}