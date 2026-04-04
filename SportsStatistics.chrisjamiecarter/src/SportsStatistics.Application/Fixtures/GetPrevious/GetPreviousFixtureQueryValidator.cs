using FluentValidation;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetPrevious;

internal sealed class GetPreviousFixtureQueryValidator
    : AbstractValidator<GetPreviousFixtureQuery>
{
    public GetPreviousFixtureQueryValidator()
    {
        RuleFor(query => query.TodayStart)
            .NotEmpty().WithError(GetPreviousFixtureQueryErrors.TodayStartIsRequired);
    }
}
