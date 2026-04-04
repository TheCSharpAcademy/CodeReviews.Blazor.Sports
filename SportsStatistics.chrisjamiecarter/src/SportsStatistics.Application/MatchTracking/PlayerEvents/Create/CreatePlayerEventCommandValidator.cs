using FluentValidation;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.PlayerEvents.Create;

internal sealed class CreatePlayerEventCommandValidator : AbstractValidator<CreatePlayerEventCommand>
{
    public CreatePlayerEventCommandValidator()
    {
        RuleFor(c => c.FixtureId)
            .NotEmpty().WithError(PlayerEventErrors.FixtureIdIsRequired);

        RuleFor(c => c.PlayerId)
            .NotEmpty().WithError(PlayerEventErrors.PlayerIdIsRequired);

        RuleFor(c => c.PlayerEventTypeId)
            .Must(PlayerEventType.ContainsValue).WithError(MatchEventBaseErrors.PlayerEventType.Unknown);

        RuleFor(c => c.BaseMinute)
            .GreaterThanOrEqualTo(Minute.MinMinute).WithError(MinuteErrors.BelowMinimum);

        RuleFor(c => c.BaseMinute)
            .LessThanOrEqualTo(Minute.MaxBaseMinute).WithError(MinuteErrors.AboveMaximum);

        RuleFor(c => c.StoppageMinute)
            .GreaterThanOrEqualTo(Minute.MinMinute)
            .When(c => c.StoppageMinute.HasValue)
            .WithError(MinuteErrors.InvalidStoppageMinute);

        RuleFor(c => c)
            .Must(c => !c.StoppageMinute.HasValue || IsValidStoppageBaseMinute(c.BaseMinute))
            .When(c => c.StoppageMinute.HasValue)
            .WithError(MinuteErrors.InvalidStoppageBaseMinute);

        RuleFor(c => c.OccurredAtUtc)
            .NotEmpty().WithError(PlayerEventErrors.OccurredAtDateAndTimeIsRequired);
    }

    private static bool IsValidStoppageBaseMinute(int baseMinute) =>
        baseMinute == Minute.FirstHalfEndMinute ||
        baseMinute == Minute.SecondHalfEndMinute;
}
