using FluentValidation;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.MatchEvents.Create;

internal sealed class CreateMatchEventCommandValidator : AbstractValidator<CreateMatchEventCommand>
{
    public CreateMatchEventCommandValidator()
    {
        RuleFor(c => c.FixtureId)
            .NotEmpty().WithError(MatchEventErrors.FixtureIdIsRequired);

        RuleFor(c => c.MatchEventTypeId)
            .Must(MatchEventType.ContainsValue).WithError(MatchEventBaseErrors.MatchEventType.Unknown);

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
            .NotEmpty().WithError(MatchEventErrors.OccurredAtDateAndTimeIsRequired);
    }

    private static bool IsValidStoppageBaseMinute(int baseMinute) =>
        baseMinute == Minute.FirstHalfEndMinute ||
        baseMinute == Minute.SecondHalfEndMinute;
}
