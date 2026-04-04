using FluentValidation;
using SportsStatistics.Domain.Clubs;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Web.Pages.Admin.Clubs.Models;

internal sealed class ClubFormModelValidator : AbstractValidator<ClubFormModel>
{
    public ClubFormModelValidator()
    {
        RuleFor(club => club.Name)
            .NotEmpty().WithError(ClubErrors.Name.IsRequired)
            .MaximumLength(Name.MaxLength).WithError(ClubErrors.Name.ExceedsMaxLength);
    }
}
