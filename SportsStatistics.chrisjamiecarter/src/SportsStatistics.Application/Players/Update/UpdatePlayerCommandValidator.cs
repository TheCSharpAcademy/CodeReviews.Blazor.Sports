using FluentValidation;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Update;

internal sealed class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerCommandValidator()
    {
        RuleFor(c => c.PlayerId)
            .NotEmpty().WithError(PlayerErrors.PlayerIdIsRequired);

        RuleFor(c => c.Name)
            .NotEmpty().WithError(PlayerErrors.NameIsRequired)
            .MaximumLength(Name.MaxLength).WithError(PlayerErrors.NameExceedsMaxLength);

        RuleFor(c => c.SquadNumber)
            .InclusiveBetween(SquadNumber.MinValue, SquadNumber.MaxValue).WithError(PlayerErrors.SquadNumberOutsideRange);

        RuleFor(c => c.Nationality)
            .NotEmpty().WithError(PlayerErrors.NationalityIsRequired)
            .MaximumLength(Nationality.MaxLength).WithError(PlayerErrors.NationalityExceedsMaxLength);

        RuleFor(c => c.DateOfBirth)
            .NotEmpty().WithError(PlayerErrors.DateOfBirthIsRequired)
            .Must(dob => dob.CalculateAge() >= DateOfBirth.MinAge).WithError(PlayerErrors.DateOfBirthBelowMinAge);

        RuleFor(c => c.PositionId)
            .Must(Position.ContainsValue).WithError(PlayerErrors.PositionNotFound);

        RuleFor(c => c.LeftClub)
            .NotNull().WithError(PlayerErrors.LeftClubIsRequired);
    }
}
