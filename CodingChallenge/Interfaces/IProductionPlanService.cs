using CodingChallenge.Models;

namespace CodingChallenge.Interfaces;

public interface IProductionPlanService
{
    Task<ProductionPlanResult> GetProductionPlan(ProductionPlanRequest request);
}