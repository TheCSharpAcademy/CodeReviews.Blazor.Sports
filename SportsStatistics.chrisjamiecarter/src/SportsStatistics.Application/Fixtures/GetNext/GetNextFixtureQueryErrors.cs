using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetNext;

public static class GetNextFixtureQueryErrors
{
    public static Error TodayEndIsRequired => Error.Validation(
        "GetPreviousFixtureQuery.TodayEndIsRequired",
        "The date and time of the end of the current day is required.");
}
