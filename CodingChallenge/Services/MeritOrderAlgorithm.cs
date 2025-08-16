using CodingChallenge.Interfaces;
using CodingChallenge.Models;
using CodingChallenge.Utils;

namespace CodingChallenge.Services;

public class MeritOrderAlgorithm : IMeritOrderAlgorithm
{
    private const int INF = int.MaxValue;
    private const int SCALE = 10;
    
    private readonly ILogger<MeritOrderAlgorithm> _logger;

    private record ScaledPlant(float Cost, int Min, int Max);
    
    public MeritOrderAlgorithm(ILogger<MeritOrderAlgorithm> logger)
    {
        Ensure.NotNull(logger);
        
        _logger = logger;
    }
    
    public Task<IEnumerable<PowerPlantLoad>> ComputeLoads(IReadOnlyList<IPowerPlantInstance> powerPlants, float targetLoad)
    {
        Ensure.NotNull(powerPlants);
        
        if (targetLoad < 0)
            throw new ArgumentOutOfRangeException(nameof(targetLoad), "Target load must be non-negative.");
        if (targetLoad == 0 || powerPlants.Count == 0)
            return Task.FromResult(Enumerable.Empty<PowerPlantLoad>());
        
        return Task.FromResult(Enumerable.Empty<PowerPlantLoad>());
    }
}