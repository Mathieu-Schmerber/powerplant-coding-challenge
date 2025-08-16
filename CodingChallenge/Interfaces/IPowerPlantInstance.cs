namespace CodingChallenge.Interfaces;

public interface IPowerPlantInstance
{
    public string Name { get; }
    public float CostPerUnit { get; }
    public float MinOutput { get; }
    public float MaxOutput { get; }
}