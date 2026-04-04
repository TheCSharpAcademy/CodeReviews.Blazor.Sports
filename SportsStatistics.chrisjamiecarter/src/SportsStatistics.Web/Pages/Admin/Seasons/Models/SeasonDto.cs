namespace SportsStatistics.Web.Pages.Admin.Seasons.Models;

public sealed record SeasonDto(
    Guid Id,
    DateOnly StartDate,
    DateOnly EndDate,
    string Name);
