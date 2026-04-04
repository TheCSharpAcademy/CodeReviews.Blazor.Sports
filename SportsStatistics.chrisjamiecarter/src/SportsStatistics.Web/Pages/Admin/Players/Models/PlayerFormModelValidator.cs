using FluentValidation;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Web.Pages.Admin.Players.Models;

internal sealed class PlayerFormModelValidator : AbstractValidator<PlayerFormModel>
{
    public PlayerFormModelValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithError(PlayerErrors.NameIsRequired)
            .MaximumLength(Name.MaxLength).WithError(PlayerErrors.NameExceedsMaxLength);

        RuleFor(c => c.SquadNumber)
            .InclusiveBetween(SquadNumber.MinValue, SquadNumber.MaxValue)
            .WithError(PlayerErrors.SquadNumberOutsideRange);

        RuleFor(c => c.Nationality)
            .NotEmpty().WithError(PlayerErrors.NationalityIsRequired)
            .MaximumLength(Nationality.MaxLength).WithError(PlayerErrors.NationalityExceedsMaxLength);

        RuleFor(c => c.DateOfBirth)
            .NotEmpty().WithError(PlayerErrors.DateOfBirthIsRequired)
            .Must(dob => dob?.CalculateAge() >= DateOfBirth.MinAge)
            .WithError(PlayerErrors.DateOfBirthBelowMinAge);

        RuleFor(c => c.Position)
            .NotEmpty().WithError(PlayerErrors.PositionNotFound);

        RuleFor(c => c.LeftClub)
            .NotNull().WithError(PlayerErrors.LeftClubIsRequired);
    }
}
