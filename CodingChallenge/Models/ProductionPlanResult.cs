namespace CodingChallenge.Models;

public record struct ProductionPlanResult
{
    public bool Feasible { get; set; }
    public float Load { get; set; }
    public float TotalCost { get; set; }
    public IEnumerable<PowerPlantLoad> PowerPlantLoads { get; set; }
}

public record struct PowerPlantLoad(string Name, float Load);