using System.Text.Json.Serialization;

namespace CodingChallenge.Models; 

/// <summary>
/// Production plan computation result. 
/// </summary>
public record struct ProductionPlanResult
{
    /// <summary>
    /// Whether the production plan is feasible.
    /// </summary>
    public bool Feasible { get; init; }
    
    /// <summary>
    /// The total load.
    /// </summary>
    public float Load { get; init; }
    
    /// <summary>
    /// The computed cost.
    /// </summary>
    public float TotalCost { get; init; }
    
    /// <summary>
    /// The power plant loads.
    /// </summary>
    public IEnumerable<PowerPlantLoad> PowerPlantLoads { get; init; }
}

/// <summary>
/// Describes a power plant load.
/// </summary>
/// <param name="Name">The power plant name.</param>
/// <param name="Load">The load.</param>
public record struct PowerPlantLoad(string Name, [property: JsonPropertyName("p")] float Load);