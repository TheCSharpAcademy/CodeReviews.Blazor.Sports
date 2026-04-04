using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public static class FixtureErrors
{
    public static Error AlreadyScheduledOnDate(DateOnly date) => Error.Conflict(
        "Fixture.AlreadyScheduledOnDate",
        $"A fixture is already scheduled on {date:yyyy-MM-dd}. Only one fixture per day is allowed.");

    public static Error CannotUpdateFixtureNotScheduled => Error.Conflict(
        "Fixture.CannotUpdateFixtureNotScheduled",
        "The fixture can only be updated when the fixture is in progress.");

    public static Error CannotUpdateFixtureScoreNotInProgress => Error.Conflict(
        "Fixture.CannotUpdateFixtureScoreNotInProgress",
        "The fixture score can only be updated when the fixture is in progress.");

    public static Error CannotDeleteNonScheduledFixture => Error.Conflict(
        "Fixture.CannotDeleteNonScheduledFixture",
        "The fixture can only be deleted when the fixture has a scheduled status.");

    public static Error KickoffTimeOutsideSeason(DateTime kickoffTimeUtc, DateOnly startDate, DateOnly endDate) => Error.Failure(
        "Fixture.KickoffTimeOutsideSeason",
        $"The fixture's kickoff time '{kickoffTimeUtc}' must be inside of the season '{startDate}/{endDate}'.");

    public static Error NoneFound => Error.Failure(
        "Fixture.NoneFound",
        "No fixture was found.");

    public static Error NotFound(Guid id) => Error.NotFound(
        "Fixture.NotFound",
        $"The fixture with the Id = '{id}' was not found.");

    public static Error SeasonIdIsRequired => Error.Validation(
        "Fixture.SeasonIdIsRequired",
        "The season identifier is required.");

    public static class CompetitionId
    {
        public static Error IsRequired => Error.Validation(
            "Fixture.CompetitionId.IsRequired",
            "The fixture competition identifier is required.");
    }

    public static class Id
    {
        public static Error IsRequired => Error.Validation(
            "Fixture.Id.IsRequired",
            "The fixture identifier is required.");
    }

    public static class KickoffTimeUtc
    {
        public static Error IsRequired => Error.Validation(
            "Fixture.KickoffTimeUtc.IsRequired",
            "The fixture kickoff time is required.");

        public static Error NullOrEmpty => Error.Validation(
            "Fixture.Opponent.KickoffTimeUtc",
            "The fixture kickoff time cannot be null or empty.");
    }

    public static class Location
    {
        public static Error NotFound => Error.Validation(
            "Fixture.Location.NotFound",
            "The fixture location with the specified identifier was not found.");
    }

    public static class Opponent
    {
        public static Error ExceedsMaxLength => Error.Validation(
            "Fixture.Opponent.ExceedsMaxLength",
            "The fixture opponent exceeds the maximum allowed length.");

        public static Error IsRequired => Error.Validation(
            "Fixture.Opponent.IsRequired",
            "The fixture opponent is required.");

        public static Error NullOrEmpty => Error.Validation(
            "Fixture.Opponent.NullOrEmpty",
            "The fixture opponent cannot be null or empty.");
    }

    public static class SeasonId
    {
        public static Error IsRequired => Error.Validation(
            "Fixture.SeasonId.IsRequired",
            "The fixture season identifier is required.");
    }

    public static class Score
    {
        public static Error AwayGoalsMustBeNonNegative => Error.Validation(
            "Fixture.Score.AwayGoalsMustBeNonNegative",
            "Away goals must be greater than or equal to zero.");
        
        public static Error HomeGoalsMustBeNonNegative => Error.Validation(
            "Fixture.Score.HomeGoalsMustBeNonNegative",
            "Home goals must be greater than or equal to zero.");

        public static Error IsRequired => Error.Validation(
            "Fixture.Score.IsRequired",
            "The fixture score is required.");
    }

    public static class Status
    {
        public static Error CannotUpdate(string from, string to) => Error.Conflict(
            "Fixture.Status.CannotUpdate",
            $"The fixture status cannot be updated from {from} to {to}.");

        public static Error IsRequired => Error.Validation(
            "Fixture.Status.IsRequired",
            "The fixture status is required.");

        public static Error Unknown => Error.Validation(
            "Fixture.Status.Unknown",
            "The fixture status cannot be inferred from the name.");
    }
}
