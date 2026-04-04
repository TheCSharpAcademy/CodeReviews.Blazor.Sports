using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.GetTopAssistsBySeason;

public static class GetTopAssistsBySeasonQueryErrors
{
    public static Error CountLessThanOrEqualToZero => Error.Validation(
        "GetTopAssistsBySeasonQuery.CountLessThanOrEqualToZero",
        "The count value must be greater than zero.");

    public static Error SeasonIdIsRequired => Error.Validation(
        "GetTopAssistsBySeasonQuery.SeasonIdIsRequired",
        "The season identifier is required.");
}
