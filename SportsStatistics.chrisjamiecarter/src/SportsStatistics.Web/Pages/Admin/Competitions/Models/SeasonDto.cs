using SportsStatistics.Application.Seasons.GetById;

namespace SportsStatistics.Web.Pages.Admin.Competitions.Models;

public sealed record SeasonDto(
    Guid Id,
    DateOnly StartDate,
    DateOnly EndDate,
    string Name)
{
    public SeasonDto(SeasonResponse season) 
        : this(season.Id, season.StartDate, season.EndDate, season.Name) { }
}

