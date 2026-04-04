namespace SportsStatistics.Web.Pages.Home.Models;

public sealed record RecentFormItemDto(
    string Result,
    string Opponent,
    int HomeGoals,
    int AwayGoals);
