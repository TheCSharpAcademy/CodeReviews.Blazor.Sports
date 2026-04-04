using FluentValidation;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Create;

internal sealed class CreateFixtureCommandValidator : AbstractValidator<CreateFixtureCommand>
{
    public CreateFixtureCommandValidator()
    {
        RuleFor(c => c.CompetitionId)
            .NotEmpty().WithError(FixtureErrors.CompetitionId.IsRequired);

        RuleFor(c => c.Opponent)
            .NotEmpty().WithError(FixtureErrors.Opponent.IsRequired)
            .MaximumLength(Opponent.MaxLength).WithError(FixtureErrors.Opponent.ExceedsMaxLength);

        RuleFor(c => c.KickoffTimeUtc)
            .NotEmpty().WithError(FixtureErrors.KickoffTimeUtc.IsRequired);

        RuleFor(c => c.LocationId)
            .Must(Location.ContainsValue).WithError(FixtureErrors.Location.NotFound);
    }
}
