using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.PlayerEvents;

public static class PlayerEventErrors
{
    public static Error FixtureIdIsRequired => Error.Validation(
        "PlayerEvent.FixtureIdIsRequired",
        "The fixture identifier is required.");

    public static Error PlayerIdIsRequired => Error.Validation(
        "PlayerEvent.PlayerIdIsRequired",
        "The player identifier is required.");

    public static Error MinuteIsRequired => Error.Validation(
        "PlayerEvent.MinuteIsRequired",
        "The fixture identifier is required.");

    public static Error OccurredAtDateAndTimeIsRequired => Error.Validation(
        "PlayerEvent.OccurredAtDateAndTimeIsRequired",
        "The occured at date and time of the player event is required.");
}
