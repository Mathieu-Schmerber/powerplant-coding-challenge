namespace CodingChallenge.Models;

public record ProductionPlanRequest
{
    public float Load { get; set; }
    public Fuels Fuels { get; set; }
    public PowerPlantDefinition[] PowerPlants { get; set; }
}

public record Fuels
{
    public float Gas { get; set; }
    public float Kerosine { get; set; }
    public float Co2 { get; set; }
    public float Wind { get; set; }
}

public record PowerPlantDefinition
{
    public string Name { get; set; }
    public string Type { get; set; }
    public float Efficiency { get; set; }
    public float Pmin { get; set; }
    public float Pmax { get; set; }
}