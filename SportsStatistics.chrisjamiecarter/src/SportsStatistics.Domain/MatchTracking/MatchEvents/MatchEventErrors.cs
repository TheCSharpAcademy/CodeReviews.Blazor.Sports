using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.MatchEvents;

public static class MatchEventErrors
{
    public static Error FixtureIdIsRequired => Error.Validation(
        "MatchEvent.FixtureIdIsRequired",
        "The fixture identifier is required.");

    public static Error MinuteIsRequired => Error.Validation(
        "MatchEvent.MinuteIsRequired",
        "The fixture identifier is required.");

    public static Error OccurredAtDateAndTimeIsRequired => Error.Validation(
        "MatchEvent.OccurredAtDateAndTimeIsRequired",
        "The occured at date and time of the match event is required.");
}
