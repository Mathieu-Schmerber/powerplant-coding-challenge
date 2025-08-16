using CodingChallenge.Configurations;
using CodingChallenge.Factories;
using CodingChallenge.Models;
using CodingChallenge.Models.PowerPlants;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace CodingChallenge.Tests.Factories;

[TestClass]
public class PowerPlantFactoryTests
{
    private PowerPlantFactory _factory;
    private Mock<IOptions<PowerPlantConfig>> _mockConfig;
    private Mock<ILogger<PowerPlantFactory>> _mockLogger;
    private PowerPlantConfig _powerPlantConfig;

    [TestInitialize]
    public void Initialize()
    {
        _mockConfig = new Mock<IOptions<PowerPlantConfig>>();
        _mockLogger = new Mock<ILogger<PowerPlantFactory>>();

        _powerPlantConfig = new PowerPlantConfig
        {
            ["gasfired"] = new PowerPlantEntry { Type = typeof(GasFiredPlantInstance).AssemblyQualifiedName },
            ["turbojet"] = new PowerPlantEntry { Type = typeof(TurboJetPlantInstance).AssemblyQualifiedName },
            ["windturbine"] = new PowerPlantEntry { Type = typeof(WindParkInstance).AssemblyQualifiedName }
        };

        _mockConfig.Setup(x => x.Value).Returns(_powerPlantConfig);
        _factory = new PowerPlantFactory(_mockConfig.Object, _mockLogger.Object);
    }

    private PowerPlantDefinition CreateDefinition(string type) => new PowerPlantDefinition(
        "Name",
        type,
        0.5f,
        0,
        100);
    
    private Fuels CreateFuels() => new Fuels(0, 0, 0, 0);
    
    [TestMethod]
    public void PowerPlantFactory_CreateInstance_GasFiredPlantInstance()
    {
        var powerPlant = _factory.CreateInstance(CreateDefinition("gasfired"), CreateFuels());
        Assert.AreEqual(typeof(GasFiredPlantInstance), powerPlant.GetType());
    }
    
    [TestMethod]
    public void PowerPlantFactory_CreateInstance_TurboJetPlantInstance()
    {
        var powerPlant = _factory.CreateInstance(CreateDefinition("turbojet"), CreateFuels());
        Assert.AreEqual(typeof(TurboJetPlantInstance), powerPlant.GetType());
    }
    
    [TestMethod]
    public void PowerPlantFactory_CreateInstance_WindParkInstance()
    {
        var powerPlant = _factory.CreateInstance(CreateDefinition("windturbine"), CreateFuels());
        Assert.AreEqual(typeof(WindParkInstance), powerPlant.GetType());
    }
}