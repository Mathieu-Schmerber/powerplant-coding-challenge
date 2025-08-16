using CodingChallenge.Models;
using CodingChallenge.Models.Exceptions;
using CodingChallenge.Models.PowerPlants;
using CodingChallenge.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace CodingChallenge.Tests.Services;

[TestClass]
public class MeritOrderAlgorithmTests
{
    private Mock<ILogger<MeritOrderAlgorithm>> _mockLogger;
    private MeritOrderAlgorithm _algorithm;
    
    /// <summary>
    /// A windpark that can produce 100 MWh.
    /// </summary>
    private WindParkInstance _maxedOutWindpark = new WindParkInstance(
        new PowerPlantDefinition("Name", "windturbine", 1f, 0, 100), 
        new Fuels(30, 30, 0, 100));

    [TestInitialize]
    public void Setup()
    {
        _mockLogger = new Mock<ILogger<MeritOrderAlgorithm>>();
        _algorithm = new MeritOrderAlgorithm(_mockLogger.Object);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task InvalidLoads()
    {
        await _algorithm.ComputeLoads([_maxedOutWindpark], 0);
        await _algorithm.ComputeLoads([_maxedOutWindpark], -100);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NoSolutionFoundException))]
    public async Task NotEnoughProductionCapacity()
    {
        await _algorithm.ComputeLoads([_maxedOutWindpark], _maxedOutWindpark.MaxOutput + 1);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NoSolutionFoundException))]
    public async Task TooMuchProductionCapacity()
    {
        await _algorithm.ComputeLoads([_maxedOutWindpark], _maxedOutWindpark.MaxOutput - 1);
    }
}