using FluentValidation;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetCompletedBySeasonId;

internal sealed class GetCompletedFixturesBySeasonIdQueryValidator
    : AbstractValidator<GetCompletedFixturesBySeasonIdQuery>
{
    public GetCompletedFixturesBySeasonIdQueryValidator()
    {
        RuleFor(c => c.SeasonId)
            .NotEmpty().WithError(GetCompletedFixturesBySeasonIdQueryErrors.SeasonIdIsRequired);
    }
}
