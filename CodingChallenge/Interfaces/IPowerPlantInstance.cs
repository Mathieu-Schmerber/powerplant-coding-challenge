namespace CodingChallenge.Interfaces;

/// <summary>
/// Power plant instance for runtime purposes. 
/// </summary>
public interface IPowerPlantInstance
{
    /// <summary>
    /// The name of the power plant.
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// The cost per MWh of electricity produced by the power plant.
    /// </summary>
    public float CostPerMWh { get; }
    
    /// <summary>
    /// The minimum output of the power plant.
    /// </summary>
    public float MinOutput { get; }
    
    /// <summary>
    /// The maximum output of the power plant.
    /// </summary>
    public float MaxOutput { get; }
}