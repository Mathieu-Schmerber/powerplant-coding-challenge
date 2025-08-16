using CodingChallenge.Interfaces;
using CodingChallenge.Models;
using CodingChallenge.Utils;

namespace CodingChallenge.Services;

/// <summary>
/// Merit order algorithm implementation using dynamic programming.
/// <see cref="IMeritOrderAlgorithm"/> 
/// </summary>
public class MeritOrderAlgorithm : IMeritOrderAlgorithm
{
    /// <summary>
    /// Infinity constant.
    /// </summary>
    private const int INF = int.MaxValue;
    
    /// <summary>
    /// Scale factor, allows for 0.1 precision.
    /// </summary>
    private const int SCALE = 10;
    
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<MeritOrderAlgorithm> _logger;

    /// <summary>
    /// Scaled power plant information.
    /// </summary>
    private record ScaledPlant(float CostPerUnit, int Min, int Max);
    
    /// <summary>
    /// Initializes a new instance of the <see cref="MeritOrderAlgorithm"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public MeritOrderAlgorithm(ILogger<MeritOrderAlgorithm> logger)
    {
        Ensure.NotNull(logger);
        
        _logger = logger;
    }
    
    /// <inheritdoc />
    public Task<IEnumerable<PowerPlantLoad>> ComputeLoads(IReadOnlyList<IPowerPlantInstance> powerPlants, float targetLoad)
    {
        Ensure.NotNull(powerPlants);
        
        if (targetLoad < 0)
            throw new ArgumentOutOfRangeException(nameof(targetLoad), "Target load must be non-negative.");
        if (targetLoad == 0 || powerPlants.Count == 0)
            return Task.FromResult(Enumerable.Empty<PowerPlantLoad>());
        
        var plantCount = powerPlants.Count;
        var steps = (int)Math.Round(targetLoad * SCALE);
        var scaledPlants = powerPlants
            .Select(p => new ScaledPlant(
                CostPerUnit: p.CostPerMWh,
                Min: (int)Math.Round(p.MinOutput * SCALE),
                Max: (int)Math.Round(p.MaxOutput * SCALE)))
            .ToArray();

        var dp = new int[plantCount + 1, steps + 1];
        var choice = new int[plantCount + 1, steps + 1];
        
        for (var i = 0; i <= plantCount; i++)
        {
            for (var load = 0; load <= steps; load++)
            {
                dp[i, load] = INF;
                choice[i, load] = -1;
            }
        }

        dp[0, 0] = 0;
        for (var i = 1; i <= plantCount; i++)
        {
            var (cost, minL, maxL) = scaledPlants[i - 1];

            for (var load = 0; load <= steps; load++)
            {
                if (dp[i - 1, load] < dp[i, load])
                {
                    dp[i, load] = dp[i - 1, load];
                    choice[i, load] = 0;
                }

                for (var x = minL; x <= maxL; x++)
                {
                    var prevLoad = load - x;
                    if (prevLoad >= 0 && dp[i - 1, prevLoad] != INF)
                    {
                        var newCost = dp[i - 1, prevLoad] + (int)(x * cost);
                        if (newCost < dp[i, load])
                        {
                            dp[i, load] = newCost;
                            choice[i, load] = x;
                        }
                    }
                }
            }
        }

        if (dp[plantCount, steps] == INF)
            throw new InvalidOperationException("No solution found for the target load.");

        var result = new PowerPlantLoad[plantCount];
        var remaining = steps;

        for (var i = plantCount; i >= 1; i--)
        {
            var load = choice[i, remaining];
            result[i - 1] = new PowerPlantLoad(
                powerPlants[i - 1].Name, 
                (float)Math.Round(load / (float)SCALE, 1));
            remaining -= load;
        }

        return Task.FromResult(result.AsEnumerable());
    }
}