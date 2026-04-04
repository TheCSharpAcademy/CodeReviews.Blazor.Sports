using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public static class CompetitionErrors
{
    public static Error ContainsNonScheduledFixtures => Error.Conflict(
        "Competition.ContainsNonScheduledFixtures",
        "The competition cannot be deleted because it contains non-scheduled fixtures.");

    public static Error NotFound(Guid id) => Error.NotFound(
        "Competition.NotFound",
        $"The competition with the identifier '{id}' was not found.");

    public static class Format
    {
        public static Error NotFound => Error.Validation(
            "Competition.Format.NotFound",
            "The competition format with the specified identifier was not found.");
    }

    public static class Id
    {
        public static Error IsRequired => Error.Validation(
            "Competition.Id.IsRequired",
            "The competition identifier is required.");
    }

    public static class Name
    {
        public static Error ExceedsMaxLength => Error.Validation(
            "Competition.Name.ExceedsMaxLength",
            "The competition name exceeds the maximum allowed length.");

        public static Error IsRequired => Error.Validation(
            "Fixture.Name.IsRequired",
            "The fixture name is required.");

        public static Error NullOrEmpty => Error.Validation(
            "Competition.Name.NullOrEmpty",
            "The competition name cannot be null or empty.");
    }

    public static class SeasonId
    {
        public static Error IsRequired => Error.Validation(
            "Competition.SeasonId.IsRequired",
            "The competition season identifier is required.");
    }
}
