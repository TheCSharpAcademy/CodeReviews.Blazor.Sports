using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Teamsheets;

public static class TeamsheetErrors
{
    public static Error NotFound(Guid id) => Error.NotFound(
        "Teamsheet.NotFound",
        $"The teamsheet with the Id = '{id}' was not found.");

    public static class FixtureId
    {
        public static readonly Error Required = Error.Validation(
            "Teamsheet.FixtureId.Required",
            "Fixture identifier is required.");

        public static readonly Error NotFound = Error.NotFound(
            "Teamsheet.FixtureId.NotFound",
            "Fixture not found.");

        public static readonly Error AlreadyHasTeamsheet = Error.Conflict(
            "Teamsheet.FixtureId.AlreadyHasTeamsheet",
            "A teamsheet has already been submitted for this fixture.");
    }

    public static class StarterIds
    {
        public static readonly Error Required = Error.Validation(
            "Teamsheet.StarterIds.Required",
            "Starter identifiers are required.");

        public static readonly Error InvalidCount = Error.Validation(
            "Teamsheet.StarterIds.InvalidCount",
            $"Teamsheet must contain exactly {Teamsheet.RequiredNumberOfStarters} starters.");

        public static readonly Error DuplicatePlayer = Error.Validation(
            "Teamsheet.StarterIds.DuplicatePlayer",
            "A player cannot be selected more than once.");

        public static readonly Error PlayerNotFound = Error.NotFound(
            "Teamsheet.StarterIds.PlayerNotFound",
            "One or more selected players do not exist.");
    }
}
