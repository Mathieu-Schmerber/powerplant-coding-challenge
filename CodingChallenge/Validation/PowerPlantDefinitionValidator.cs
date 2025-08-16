using CodingChallenge.Models;
using FluentValidation;

namespace CodingChallenge.Validation;

/// <summary>
/// Validator for <see cref="PowerPlantDefinition"/>.
/// </summary>
public class PowerPlantDefinitionValidator : AbstractValidator<PowerPlantDefinition>
{
    public PowerPlantDefinitionValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Power plant must have a non-empty name.");

        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("Power plant '{PropertyName}' must have a non-empty type.")
            .OverridePropertyName("Type");

        RuleFor(x => x.Efficiency)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Power plant efficiency must be greater than or equal to 0.")
            .LessThanOrEqualTo(1)
            .WithMessage("Power plant efficiency must be less than or equal to 1.");

        RuleFor(x => x.Pmin)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Power plant Pmin must be greater than or equal to 0.");

        RuleFor(x => x.Pmax)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Power plant Pmax must be greater than or equal to 0.");

        RuleFor(x => x)
            .Must(x => x.Pmin <= x.Pmax)
            .WithMessage("Power plant Pmin ({Pmin}) cannot be greater than Pmax ({Pmax})")
            .When(x => x.Pmin > 0 && x.Pmax > 0);
    }
}