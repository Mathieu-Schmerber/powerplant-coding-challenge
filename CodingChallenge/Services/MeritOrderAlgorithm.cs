using System.Diagnostics;
using CodingChallenge.Interfaces;
using CodingChallenge.Models;
using CodingChallenge.Models.Exceptions;
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
    /// The stopwatch for performance measurements.
    /// </summary>
    private readonly Stopwatch _stopwatch;

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
        _stopwatch = new Stopwatch();
    }
    
    /// <inheritdoc />
    public Task<IEnumerable<PowerPlantLoad>> ComputeLoads(IReadOnlyList<IPowerPlantInstance> powerPlants, float targetLoad)
    {
        Ensure.NotNull(powerPlants);
        
        if (targetLoad <= 0)
            throw new ArgumentException("The target load must be greater than 0.", nameof(targetLoad));
        else if (powerPlants.Count == 0)
            throw new ArgumentException("No power plants provided.", nameof(powerPlants));
        
        _stopwatch.Start();
        
        var scaledTargetLoad = ScaleLoad(targetLoad);
        var scaledPlants = powerPlants
            .Select(plant => new ScaledPlant(
                CostPerUnit: plant.CostPerMWh,
                Min: ScaleLoad(plant.MinOutput),
                Max: ScaleLoad(plant.MaxOutput)))
            .ToArray();
        
        var choiceTable = Solve(scaledPlants, scaledTargetLoad);
        var result = ReconstructSolution(powerPlants, choiceTable, scaledTargetLoad);
        
        _stopwatch.Stop();
        _logger.LogInformation("Computed production plan in: {StopwatchElapsedMilliseconds} ms.", _stopwatch.ElapsedMilliseconds);
        return Task.FromResult(result.AsEnumerable());
    }

    /// <summary>
    /// Scales the load using the scale factor.
    /// </summary>
    private static int ScaleLoad(float load) 
        => (int)Math.Round(load * SCALE);
    
    /// <summary>
    /// Initializes the dynamic programming tables.
    /// </summary>
    private static (int[,] dp, int[,] choice) InitializeTables(int plantCount, int maxLoad)
    {
        var dp = new int[plantCount + 1, maxLoad + 1];
        var choice = new int[plantCount + 1, maxLoad + 1];
        
        for (var i = 0; i <= plantCount; i++)
        {
            for (var load = 0; load <= maxLoad; load++)
            {
                dp[i, load] = INF;
                choice[i, load] = -1;
            }
        }

        dp[0, 0] = 0;
        return (dp, choice);
    }

    /// <summary>
    /// Solves the production plan.
    /// </summary>
    private static int[,] Solve(ScaledPlant[] scaledPlants, int targetLoad)
    {
        var plantCount = scaledPlants.Length;
        var (dp, choice) = InitializeTables(plantCount, targetLoad);

        for (var plantIndex = 1; plantIndex <= plantCount; plantIndex++)
        {
            var plant = scaledPlants[plantIndex - 1];
            
            for (var currentLoad = 0; currentLoad <= targetLoad; currentLoad++)
            {
                var costWithoutPlant = dp[plantIndex - 1, currentLoad];
                if (costWithoutPlant < dp[plantIndex, currentLoad])
                {
                    dp[plantIndex, currentLoad] = costWithoutPlant;
                    choice[plantIndex, currentLoad] = 0;
                }
                
                for (var plantOutput = plant.Min; plantOutput <= plant.Max; plantOutput++)
                {
                    var remainingLoad = currentLoad - plantOutput;
                    if (remainingLoad < 0 || dp[plantIndex - 1, remainingLoad] == INF)
                        continue;

                    var plantCost = (int)(plantOutput * plant.CostPerUnit);
                    var totalCost = dp[plantIndex - 1, remainingLoad] + plantCost;
                    if (totalCost < dp[plantIndex, currentLoad])
                    {
                        dp[plantIndex, currentLoad] = totalCost;
                        choice[plantIndex, currentLoad] = plantOutput;
                    }
                }
            }
        }

        if (dp[plantCount, targetLoad] == INF)
            throw new NoSolutionFoundException($"No solution found for the target load: {targetLoad}.");

        return choice;
    }

    /// <summary>
    /// Reconstructs the optimal solution from the choice table.
    /// </summary>
    private static PowerPlantLoad[] ReconstructSolution(
        IReadOnlyList<IPowerPlantInstance> originalPlants,
        int[,] choice,
        int targetLoad)
    {
        var result = new PowerPlantLoad[originalPlants.Count];
        var remainingLoad = targetLoad;

        for (var plantIndex = originalPlants.Count; plantIndex >= 1; plantIndex--)
        {
            var scaledOutput = choice[plantIndex, remainingLoad];
            var actualOutput = (float)Math.Round(scaledOutput / (float)SCALE, 1);
            
            result[plantIndex - 1] = new PowerPlantLoad(
                originalPlants[plantIndex - 1].Name, 
                actualOutput);
                
            remainingLoad -= scaledOutput;
        }

        return result;
    }
}