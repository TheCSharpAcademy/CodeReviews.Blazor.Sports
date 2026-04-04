using FluentValidation;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetByDate;

internal sealed class GetFixturesByDateQueryValidator : AbstractValidator<GetFixturesByDateQuery>
{
    public GetFixturesByDateQueryValidator()
    {
        RuleFor(fixture => fixture.FixtureDate)
            .NotEmpty().WithError(GetFixturesByDateQueryErrors.FixtureDateIsRequired);
    }
}
