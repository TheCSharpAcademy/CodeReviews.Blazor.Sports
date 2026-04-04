using SportsStatistics.Application.Seasons.GetAll;

namespace SportsStatistics.Web.Pages.Reports.Models;

internal static class SeasonMapper
{
    public static List<SeasonDto> ToDto(this IEnumerable<SeasonResponse> seasons) =>
        [.. seasons.Select(ToDto)];

    public static SeasonDto ToDto(this SeasonResponse season) => new(
        season.Id,
        season.StartDate,
        season.EndDate,
        season.Name);
}
