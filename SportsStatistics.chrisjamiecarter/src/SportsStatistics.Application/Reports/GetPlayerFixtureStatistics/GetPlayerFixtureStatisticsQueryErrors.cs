using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Reports.GetPlayerFixtureStatistics;

public static class GetPlayerFixtureStatisticsQueryErrors
{
    public static Error FixtureIdIsRequired => Error.Validation(
        "GetPlayerGameStatisticsErrors.FixtureIdIsRequired",
        "The fixture identifier is required.");
}
