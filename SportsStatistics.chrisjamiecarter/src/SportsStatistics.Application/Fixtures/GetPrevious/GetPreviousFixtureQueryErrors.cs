using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetPrevious;

public static class GetPreviousFixtureQueryErrors
{
    public static Error TodayStartIsRequired => Error.Validation(
        "GetPreviousFixtureQuery.TodayStartIsRequired",
        "The date and time of the start of the current day is required.");
}
