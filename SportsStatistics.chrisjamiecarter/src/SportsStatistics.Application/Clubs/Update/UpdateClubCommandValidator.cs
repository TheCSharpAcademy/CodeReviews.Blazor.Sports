using FluentValidation;
using SportsStatistics.Domain.Clubs;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Clubs.Update;

internal sealed class UpdateClubCommandValidator : AbstractValidator<UpdateClubCommand>
{
    public UpdateClubCommandValidator()
    {
        RuleFor(c => c.ClubId)
            .NotEmpty().WithError(ClubErrors.Id.IsRequired);

        RuleFor(c => c.Name)
            .NotEmpty().WithError(ClubErrors.Name.IsRequired)
            .MaximumLength(Name.MaxLength).WithError(ClubErrors.Name.ExceedsMaxLength);
    }
}
