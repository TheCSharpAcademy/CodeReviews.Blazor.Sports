using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.GetTopScorersBySeason;

public static class GetTopScorersBySeasonQueryErrors
{
    public static Error CountLessThanOrEqualToZero => Error.Validation(
        "GetTopScorersBySeasonQuery.CountLessThanOrEqualToZero",
        "The count value must be greater than zero.");

    public static Error SeasonIdIsRequired => Error.Validation(
        "GetTopScorersBySeasonQuery.SeasonIdIsRequired",
        "The season identifier is required.");
}
