using CodingChallenge.Interfaces;
using CodingChallenge.Models;
using CodingChallenge.Utils;

namespace CodingChallenge.Services;

public class ProductionPlanService : IProductionPlanService
{
    private readonly IPowerPlantFactory _powerPlantFactory;
    private readonly IMeritOrderAlgorithm _algorithm;
    private readonly ILogger<ProductionPlanService> _logger;

    public ProductionPlanService(
        IPowerPlantFactory powerPlantFactory,
        IMeritOrderAlgorithm algorithm, 
        ILogger<ProductionPlanService> logger)
    {
        Ensure.NotNull(powerPlantFactory);
        Ensure.NotNull(algorithm);
        Ensure.NotNull(logger);

        _powerPlantFactory = powerPlantFactory;
        _algorithm = algorithm;
        _logger = logger;
    }
    
    public async Task<ProductionPlanResult> GetProductionPlan(ProductionPlanRequest request)
    {
        Ensure.NotNull(request);
        
        var powerPlantInstances = request.PowerPlants
            .Select(x => _powerPlantFactory.CreateInstance(x, request.Fuels))
            .ToList();

        try
        {
            var productionPlan = await _algorithm.ComputeLoads(powerPlantInstances, request.Load);
            return new ProductionPlanResult {Feasible = true, PowerPlantLoads = productionPlan};
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to compute production plan.");
            return new ProductionPlanResult {Feasible = false};
        }
    }
}