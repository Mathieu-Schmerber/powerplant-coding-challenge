using System.Text.Json.Serialization;

namespace CodingChallenge.Models;

/// <summary>
/// The production plan request DTO.
/// </summary>
/// <param name="Load">The targeted load.</param>
/// <param name="Fuels">The fuels' costs.</param>
/// <param name="PowerPlants">The available power plants.</param>
public record ProductionPlanRequest(float Load, Fuels Fuels, PowerPlantDefinition[] PowerPlants);

/// <summary>
/// The fuels' costs.
/// </summary>
/// <param name="GasCost">The gas cost in euros per MWh.</param>
/// <param name="KerosineCost">The kerosine cost in euros per MWh.</param>
/// <param name="Co2Cost">The co2 cost in euros per ton.</param>
/// <param name="WindPercentage">The wind percentage.</param>
public record Fuels(
    [property: JsonPropertyName("gas(euro/MWh)")]
    float GasCost,
    
    [property: JsonPropertyName("kerosine(euro/MWh)")]
    float KerosineCost,
    
    [property: JsonPropertyName("co2(euro/ton)")]
    float Co2Cost,
    
    [property: JsonPropertyName("wind(%)")]
    float WindPercentage);

/// <summary>
/// The power plant definition.
/// </summary>
/// <param name="Name">The name of the power plant.</param>
/// <param name="Type">The type of the power plant.</param>
/// <param name="Efficiency">The efficiency of the power plant.</param>
/// <param name="Pmin">The minimum production of the power plant.</param>
/// <param name="Pmax">The maximum production of the power plant.</param>
public record PowerPlantDefinition(string Name, string Type, float Efficiency, float Pmin, float Pmax);