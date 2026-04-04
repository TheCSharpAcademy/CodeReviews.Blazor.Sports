using FluentValidation;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;
using SportsStatistics.Web.Models;

namespace SportsStatistics.Web.Pages.Admin.Fixtures.Models;

internal sealed class FixtureFormModelValidator : AbstractValidator<FixtureFormModel>
{
    public FixtureFormModelValidator()
    {
        RuleFor(f => f.Season)
            .NotNull().WithError(FixtureFormErrors.SeasonRequired);

        RuleFor(f => f.Competition)
            .NotNull().WithError(FixtureFormErrors.CompetitionRequired);

        RuleFor(f => f.KickoffDateLocal)
            .NotNull().WithError(FixtureFormErrors.KickoffDateRequired)
            .GreaterThanOrEqualTo(model => model.Season!.StartDate.ToDateTime())
            .When(model => model.Season is not null)
            .WithErrorCode(FixtureFormErrors.KickoffDateOutsideSeason(default, default).Code)
            .WithMessage(model => $"The kickoff date must be within the season '{model.Season!.StartDate:d}' - '{model.Season!.EndDate:d}'.")
            .LessThanOrEqualTo(model => model.Season!.EndDate.ToDateTime())
            .When(model => model.Season is not null)
            .WithErrorCode(FixtureFormErrors.KickoffDateOutsideSeason(default, default).Code)
            .WithMessage(model => $"The kickoff date must be within the season '{model.Season!.StartDate:d}' - '{model.Season!.EndDate:d}'.");
        
        RuleFor(f => f.KickoffTimeLocal)
            .NotNull().WithError(FixtureFormErrors.KickoffTimeRequired);

        RuleFor(f => f.Location)
            .NotNull().WithError(FixtureFormErrors.LocationRequired);

        RuleFor(c => c.Opponent)
            .NotEmpty().WithError(FixtureFormErrors.OpponentRequired)
            .MaximumLength(Opponent.MaxLength).WithError(FixtureFormErrors.OpponentExceedsMaxLength);
    }
}
