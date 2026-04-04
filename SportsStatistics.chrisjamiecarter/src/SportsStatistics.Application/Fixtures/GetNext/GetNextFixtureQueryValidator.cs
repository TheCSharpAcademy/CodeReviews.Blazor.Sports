using FluentValidation;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetNext;

internal sealed class GetNextFixtureQueryValidator
    : AbstractValidator<GetNextFixtureQuery>
{
    public GetNextFixtureQueryValidator()
    {
        RuleFor(query => query.TodayEnd)
            .NotEmpty().WithError(GetNextFixtureQueryErrors.TodayEndIsRequired);
    }
}
