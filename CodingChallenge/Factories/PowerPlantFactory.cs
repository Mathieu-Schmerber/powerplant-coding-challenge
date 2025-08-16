using CodingChallenge.Configurations;
using CodingChallenge.Interfaces;
using CodingChallenge.Models;
using CodingChallenge.Utils;
using Microsoft.Extensions.Options;

namespace CodingChallenge.Factories;

public class PowerPlantFactory : IPowerPlantFactory
{
    private readonly IOptions<PowerPlantConfig> _config;
    private readonly ILogger<PowerPlantFactory> _logger;

    public PowerPlantFactory(
        IOptions<PowerPlantConfig> config,
        ILogger<PowerPlantFactory> logger)
    {
        Ensure.NotNull(config);
        Ensure.NotNull(logger);   
        
        _config = config;
        _logger = logger;
    }
    
    public IPowerPlantInstance CreateInstance(PowerPlantDefinition definition, Fuels fuels)
    {
        Ensure.NotNull(definition, nameof(definition));
        Ensure.NotNull(fuels, nameof(fuels));
        Ensure.NotNull(_config.Value, nameof(_config.Value));
        
        if (!_config.Value.TryGetValue(definition.Type, out var entry))
            throw new InvalidOperationException($"No power plant configured for '{definition.Type}'.");

        var type = Type.GetType(entry.Type);
        if (type == null)
            throw new InvalidOperationException($"Type '{entry.Type}' not found.");

        var instance = Activator.CreateInstance(type, definition, fuels);
        if (instance == null)
            throw new InvalidOperationException($"Failed to create instance of type '{entry.Type}'.");

        if (instance is not IPowerPlantInstance result)
            throw new InvalidOperationException($"Type '{entry.Type}' must implement '{nameof(IPowerPlantInstance)}'.");
        
        _logger.LogInformation($"Created instance of type '{entry.Type}' for power plant '{definition.Name}'.");
        return result;
    }
}