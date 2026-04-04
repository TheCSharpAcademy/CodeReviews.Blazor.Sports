using FluentValidation;
using SportsStatistics.Domain.Teamsheets;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Teamsheets.Create;

internal sealed class CreateTeamsheetCommandValidator : AbstractValidator<CreateTeamsheetCommand>
{
    public CreateTeamsheetCommandValidator()
    {
        RuleFor(c => c.FixtureId)
            .NotEmpty().WithError(TeamsheetErrors.FixtureId.Required);

        RuleFor(c => c.StarterIds)
            .NotEmpty().WithError(TeamsheetErrors.StarterIds.Required)
            .Must(ids => ids.Count == Teamsheet.RequiredNumberOfStarters).WithError(TeamsheetErrors.StarterIds.InvalidCount)
            .Must(ids => ids.Distinct().Count() == ids.Count).WithError(TeamsheetErrors.StarterIds.DuplicatePlayer);
    }
}
