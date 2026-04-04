namespace SportsStatistics.Web.Pages.Home.Models;

public sealed record TopPlayerStatsCardDto(
    string StatName,
    List<TopPlayerStatDto> PlayerStats);
