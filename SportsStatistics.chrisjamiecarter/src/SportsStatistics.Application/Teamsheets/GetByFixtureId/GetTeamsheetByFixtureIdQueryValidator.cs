using FluentValidation;

namespace SportsStatistics.Application.Teamsheets.GetByFixtureId;

internal sealed class GetTeamsheetByFixtureIdQueryValidator : AbstractValidator<GetTeamsheetByFixtureIdQuery>
{
    public GetTeamsheetByFixtureIdQueryValidator()
    {
        RuleFor(query => query.FixtureId)
            .NotEmpty();
    }
}
