using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Seasons;

public static class SeasonErrors
{
    public static Error ContainsNonScheduledFixtures => Error.Conflict(
        "Season.ContainsNonScheduledFixtures",
        "The season cannot be deleted because it contains non-scheduled fixtures.");

    public static Error NoCurrentSeasonFound(DateOnly today) => Error.Failure(
        "Season.NoCurrentSeasonFound",
        $"A season for '{today}' was not found.");

    public static Error NotCreated(DateOnly startDate, DateOnly endDate) => Error.Failure(
        "Season.NotCreated",
        $"The season with the StartDate = '{startDate}' and EndDate = '{endDate}' was not created.");
    
    public static Error NotDeleted(Guid id) => Error.Failure(
        "Season.NotDeleted",
        $"The season with the Id = '{id}' was not deleted.");

    public static Error NotFound(Guid id) => Error.NotFound(
        "Season.NotFound",
        $"The season with the Id = '{id}' was not found.");

    public static Error NotUpdated(Guid id) => Error.Failure(
        "Season.NotUpdated",
        $"The season with the Id = '{id}' was not updated.");

    public static class DateRange
    {
        public static Error StartDateAfterEndDate => Error.Validation(
                "Season.DateRange.StartDateAfterEndDate",
                "The season start date cannot be after the end date.");

        public static class StartDate
        {
            public static Error NullOrEmpty => Error.Validation(
                "Season.DateRange.StartDate.NullOrEmpty",
                "The season start date cannot be null or empty.");
        }

        public static class EndDate
        {
            public static Error NullOrEmpty => Error.Validation(
                "Season.DateRange.EndDate.NullOrEmpty",
                "The season end date cannot be null or empty.");
        }
    }
}
