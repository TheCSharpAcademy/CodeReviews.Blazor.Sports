using FluentValidation;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Web.Pages.Admin.Competitions.Models;

internal sealed class CompetitionFormModelValidator : AbstractValidator<CompetitionFormModel>
{
    public CompetitionFormModelValidator()
    {
        RuleFor(c => c.Season)
            .NotEmpty().WithError(CompetitionErrors.SeasonId.IsRequired);

        RuleFor(c => c.Name)
            .NotEmpty().WithError(CompetitionErrors.Name.IsRequired)
            .MaximumLength(Name.MaxLength).WithError(CompetitionErrors.Name.ExceedsMaxLength);

        RuleFor(c => c.Format)
            .NotEmpty().WithError(CompetitionErrors.Format.NotFound);
    }
}
