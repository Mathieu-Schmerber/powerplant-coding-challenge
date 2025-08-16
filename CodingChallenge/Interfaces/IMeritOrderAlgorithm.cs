using CodingChallenge.Models;

namespace CodingChallenge.Interfaces;

/// <summary>
/// Merit order algorithm. 
/// </summary>
public interface IMeritOrderAlgorithm
{
    /// <summary>
    /// Computes a per power plant load for the given target load.
    /// </summary>
    /// <param name="powerPlants">The available power plants.</param>
    /// <param name="targetLoad">The target load.</param>
    /// <returns>The per power plant load.</returns>
    Task<IEnumerable<PowerPlantLoad>> ComputeLoads(IReadOnlyList<IPowerPlantInstance> powerPlants, float targetLoad);
}