using FluentValidation;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetBySeasonId;

internal sealed class GetFixturesBySeasonIdQueryValidator : AbstractValidator<GetFixturesBySeasonIdQuery>
{
    public GetFixturesBySeasonIdQueryValidator()
    {
        RuleFor(c => c.SeasonId)
            .NotEmpty().WithError(FixtureErrors.SeasonIdIsRequired);
    }
}
