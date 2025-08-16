using CodingChallenge.Models;
using FluentValidation;

namespace CodingChallenge.Validation;

/// <summary>
/// Validator for <see cref="ProductionPlanRequest"/>.
/// </summary>
public class ProductionPlanRequestValidator : AbstractValidator<ProductionPlanRequest>
{
    public ProductionPlanRequestValidator()
    {
        RuleFor(x => x.Load)
            .GreaterThan(0)
            .WithMessage("Load must be positive and greater than 0.");

        RuleFor(x => x.Fuels)
            .NotNull()
            .WithMessage("Fuels data cannot be null.")
            .SetValidator(new FuelsValidator());

        RuleFor(x => x.PowerPlants)
            .NotNull()
            .WithMessage("Power plants array cannot be null.")
            .NotEmpty()
            .WithMessage("At least one power plant must be provided.");

        RuleForEach(x => x.PowerPlants)
            .NotNull()
            .WithMessage("Power plant cannot be null.")
            .SetValidator(new PowerPlantDefinitionValidator());

        RuleFor(x => x.PowerPlants)
            .Must(HaveUniqueNames)
            .WithMessage("All power plant names must be unique.")
            .When(x => x.PowerPlants.Length > 0);
    }

    /// <summary>
    /// Checks if all power plant names are unique.
    /// </summary>
    /// <param name="powerPlants">The power plants.</param>
    /// <returns>True if all names are unique, false otherwise.</returns>
    private static bool HaveUniqueNames(PowerPlantDefinition[]? powerPlants)
    {
        if (powerPlants == null || powerPlants.Length == 0)
            return true;

        var names = powerPlants
            .Where(p => !string.IsNullOrWhiteSpace(p.Name))
            .Select(p => p.Name)
            .ToList();

        return names.Count == names.Distinct(StringComparer.OrdinalIgnoreCase).Count();
    }
}