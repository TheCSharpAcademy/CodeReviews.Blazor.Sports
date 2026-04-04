namespace SportsStatistics.Web.Pages.Admin.Fixtures;

public sealed record SeasonDto(
    Guid Id,
    DateOnly StartDate,
    DateOnly EndDate,
    string Name);
