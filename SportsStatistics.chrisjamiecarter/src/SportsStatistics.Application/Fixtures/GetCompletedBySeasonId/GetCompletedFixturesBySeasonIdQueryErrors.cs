using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetCompletedBySeasonId;

public static class GetCompletedFixturesBySeasonIdQueryErrors
{
    public static Error SeasonIdIsRequired => Error.Validation(
        "GetCompletedFixturesBySeasonIdQuery.SeasonIdIsRequired",
        "The season identifier is required.");
}
