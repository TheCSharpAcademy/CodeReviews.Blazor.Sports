namespace SportsStatistics.Web.Pages.Reports.Models;

public sealed record SeasonDto(
    Guid SeasonId,
    DateOnly StartDate,
    DateOnly EndDate,
    string Name);
