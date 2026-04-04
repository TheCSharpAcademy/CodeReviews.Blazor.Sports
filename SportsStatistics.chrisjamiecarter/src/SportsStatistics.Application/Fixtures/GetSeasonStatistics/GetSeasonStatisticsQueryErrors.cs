using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetSeasonStatistics;

public static class GetSeasonStatisticsQueryErrors
{
    public static Error SeasonIdIsRequired => Error.Validation(
        "GetSeasonStatisticsQuery.SeasonIdIsRequired",
        "The season identifier is required.");
}
