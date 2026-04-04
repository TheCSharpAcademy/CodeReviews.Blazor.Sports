using FluentValidation;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Web.Pages.Admin.Seasons.Models;

internal sealed class SeasonFormModelValidator : AbstractValidator<SeasonFormModel>
{
    public SeasonFormModelValidator()
    {
        RuleFor(c => c.StartDate)
            .NotEmpty().WithError(SeasonErrors.DateRange.StartDate.NullOrEmpty);

        RuleFor(c => c.EndDate)
            .NotEmpty().WithError(SeasonErrors.DateRange.EndDate.NullOrEmpty);

        RuleFor(c => c)
            .Must(c => c.StartDate < c.EndDate)
            .WithError(SeasonErrors.DateRange.StartDateAfterEndDate);
    }
}
