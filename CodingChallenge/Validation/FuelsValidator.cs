using CodingChallenge.Models;
using FluentValidation;

namespace CodingChallenge.Validation;

/// <summary>
/// Validator for <see cref="Fuels"/>.
/// </summary>
public class FuelsValidator : AbstractValidator<Fuels>
{
    public FuelsValidator()
    {
        RuleFor(x => x.GasCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Gas cost must be positive.");

        RuleFor(x => x.KerosineCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Kerosine cost must be positive.");

        RuleFor(x => x.Co2Cost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("CO2 cost must be positive.");

        RuleFor(x => x.WindPercentage)
            .InclusiveBetween(0, 100)
            .WithMessage("Wind percentage must be between 0 and 100.");
    }
}