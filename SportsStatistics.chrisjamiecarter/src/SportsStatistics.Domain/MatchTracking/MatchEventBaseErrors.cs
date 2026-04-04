using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking;

public static class MatchEventBaseErrors
{
    public static Error FixtureIdIsRequired => Error.Validation(
        "MatchEventBase.FixtureIdIsRequired",
        "The fixture identifier is required.");

    public static Error MinuteIsRequired => Error.Validation(
        "MatchEventBase.MinuteIsRequired",
        "The fixture identifier is required.");

    public static Error OccurredAtDateAndTimeIsRequired => Error.Validation(
        "MatchEventBase.OccurredAtDateAndTimeIsRequired",
        "The occured at date and time of the match event is required.");

    public static class MatchEventType
    {
        public static Error Unknown => Error.Validation(
            "MatchTracking.MatchEventType.Unknown",
            "The match event type cannot be inferred from the name.");
    }

    public static class PlayerEventType
    {
        public static Error Unknown => Error.Validation(
            "MatchTracking.PlayerEventType.Unknown",
            "The player event type cannot be inferred from the name.");
    }

    public static class SubstitutionEvent
    {
        public static Error SamePlayer => Error.Validation(
            "MatchTracking.SubstitutionEvent.SamePlayer",
            "The player being substituted out cannot be the same as the player being substituted in.");

        public static class PlayerOutId
        {
            public static Error NullOrEmpty => Error.Validation(
                "MatchTracking.SubstitutionEvent.PlayerOutId.NullOrEmpty",
                "The id of the player being substituted out cannot be null or empty.");
        }

        public static class PlayerInId
        {
            public static Error NullOrEmpty => Error.Validation(
                "MatchTracking.SubstitutionEvent.PlayerInId.NullOrEmpty",
                "The id of the player being substituted in cannot be null or empty.");
        }
    }
}
