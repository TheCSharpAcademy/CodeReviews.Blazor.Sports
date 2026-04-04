using FluentValidation;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Web.Pages.MatchTracker.Models;

internal sealed class SubstitutionFormModelValidator : AbstractValidator<SubstitutionFormModel>
{
    public SubstitutionFormModelValidator()
    {
        RuleFor(model => model.PlayerOn)
            .NotEmpty().WithError(SubstitutionEventErrors.PlayerOnIdIsRequired);
    }
}
