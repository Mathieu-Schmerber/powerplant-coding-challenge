using CodingChallenge.Interfaces;
using CodingChallenge.Models;
using CodingChallenge.Utils;

namespace CodingChallenge.Services;

/// <summary>
/// The production plan service.
/// <see cref="IProductionPlanService"/>
/// </summary>
public class ProductionPlanService : IProductionPlanService
{
    /// <summary>
    /// The power plant factory.
    /// </summary>
    private readonly IPowerPlantFactory _powerPlantFactory;
    
    /// <summary>
    /// The merit order algorithm implementation.
    /// </summary>
    private readonly IMeritOrderAlgorithm _algorithm;
    
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<ProductionPlanService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductionPlanService"/> class.
    /// </summary>
    /// <param name="powerPlantFactory">The power plant factory.</param>
    /// <param name="algorithm">The merit order algorithm implementation.</param>
    /// <param name="logger">The logger.</param>
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
    
    /// <inheritdoc />
    public async Task<ProductionPlanResult> GetProductionPlan(ProductionPlanRequest request)
    {
        Ensure.NotNull(request);
        
        var powerPlantInstances = request.PowerPlants
            .Select(x => _powerPlantFactory.CreateInstance(x, request.Fuels))
            .OrderBy(x => x.CostPerMWh)
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