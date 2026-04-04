using FluentValidation;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Update;

internal sealed class UpdateFixtureCommandValidator : AbstractValidator<UpdateFixtureCommand>
{
    public UpdateFixtureCommandValidator()
    {
        RuleFor(c => c.FixtureId)
            .NotEmpty().WithError(FixtureErrors.Id.IsRequired);

        RuleFor(c => c.Opponent)
            .NotEmpty().WithError(FixtureErrors.Opponent.IsRequired)
            .MaximumLength(Opponent.MaxLength).WithError(FixtureErrors.Opponent.ExceedsMaxLength);

        RuleFor(c => c.KickoffTimeUtc)
            .NotEmpty().WithError(FixtureErrors.KickoffTimeUtc.IsRequired);

        RuleFor(c => c.LocationId)
            .Must(Location.ContainsValue).WithError(FixtureErrors.Location.NotFound);
    }
}
