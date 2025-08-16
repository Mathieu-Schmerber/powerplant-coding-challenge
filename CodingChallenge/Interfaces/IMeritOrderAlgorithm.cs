using CodingChallenge.Models;

namespace CodingChallenge.Interfaces;

public interface IMeritOrderAlgorithm
{
    Task<IEnumerable<PowerPlantLoad>> ComputeLoads(IReadOnlyList<IPowerPlantInstance> powerPlants, float targetLoad);
}