using CodingChallenge.Models;

namespace CodingChallenge.Interfaces;

public interface IPowerPlantFactory
{
    IPowerPlantInstance CreateInstance(PowerPlantDefinition definition, Fuels fuels);
}