using CodingChallenge.Models;

namespace CodingChallenge.Interfaces;

/// <summary>
/// The production plan service. 
/// </summary>
public interface IProductionPlanService
{
    /// <summary>
    /// Gets the production plan.
    /// </summary>
    /// <param name="request">The production plan request.</param>
    /// <returns>The production plan.</returns>
    Task<ProductionPlanResult> GetProductionPlan(ProductionPlanRequest request);
}