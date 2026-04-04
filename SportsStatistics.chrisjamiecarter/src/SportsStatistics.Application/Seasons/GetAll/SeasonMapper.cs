using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Seasons.GetAll;

internal static class SeasonMapper
{
    public static List<SeasonResponse> ToResponse(this IEnumerable<Season> seasons)
        => [.. seasons.Select(ToResponse)];

    public static SeasonResponse ToResponse(this Season season)
        => new(season.Id,
               season.DateRange.StartDate,
               season.DateRange.EndDate,
               season.Name);
}
