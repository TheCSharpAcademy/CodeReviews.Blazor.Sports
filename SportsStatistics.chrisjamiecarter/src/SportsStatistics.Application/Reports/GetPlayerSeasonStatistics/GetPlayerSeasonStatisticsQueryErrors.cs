using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Reports.GetPlayerSeasonStatistics;

public static class GetPlayerSeasonStatisticsQueryErrors
{
    public static Error SeasonIdIsRequired => Error.Validation(
        "GetPlayerSeasonStatisticsQuery.SeasonIdIsRequired",
        "The season identifier is required.");
}
