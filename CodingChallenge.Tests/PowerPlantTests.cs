using CodingChallenge.Models;
using CodingChallenge.Models.PowerPlants;

namespace CodingChallenge.Tests;

[TestClass]
public class PowerPlantTests
{
    [TestMethod]
    public void TestCosts()
    {
        var fuels = new Fuels(30, 30, 0, 50);
        
        var turbojet = new TurboJetPlantInstance(
            new PowerPlantDefinition("Name", "turbojet", 0.3f, 0, 100), 
            fuels);
        Assert.AreEqual(100, turbojet.CostPerMWh);
        
        var gasFired = new GasFiredPlantInstance(
            new PowerPlantDefinition("Name", "gasfired", 0.5f, 0, 100), 
            fuels);
        Assert.AreEqual(60, gasFired.CostPerMWh);
        
        var wind = new WindParkInstance(
            new PowerPlantDefinition("Name", "windturbine", 1f, 0, 100), 
            fuels);
        Assert.AreEqual(0, wind.CostPerMWh);
    }
    
    [TestMethod]
    public void WindParkOutput()
    {
        var fuels = new Fuels(30, 30, 0, 50);
        var wind = new WindParkInstance(
            new PowerPlantDefinition("Name", "windturbine", 1f, 0, 100), 
            fuels);
        
        Assert.AreEqual(50, wind.MinOutput);
        Assert.AreEqual(50, wind.MaxOutput);
    }
}