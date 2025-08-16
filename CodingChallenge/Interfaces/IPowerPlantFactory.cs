using CodingChallenge.Models;

namespace CodingChallenge.Interfaces;

/// <summary>
/// Power plant factory. 
/// </summary>
public interface IPowerPlantFactory
{
    /// <summary>
    /// Creates an instance of a power plant.
    /// </summary>
    /// <param name="definition">The power plant definition.</param>
    /// <param name="fuels">The fuels.</param>
    /// <returns>The power plant instance.</returns>
    IPowerPlantInstance CreateInstance(PowerPlantDefinition definition, Fuels fuels);
}