namespace SportsStatistics.Web.Pages.Home.Models;

public sealed record TopPlayerStatDto(
    Guid PlayerId,
    string PlayerName,
    string PositionName,
    int StatCount);
