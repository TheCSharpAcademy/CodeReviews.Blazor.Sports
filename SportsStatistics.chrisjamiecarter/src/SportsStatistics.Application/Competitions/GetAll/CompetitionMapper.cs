using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Application.Competitions.GetAll;

internal static class CompetitionMapper
{
    public static List<CompetitionResponse> ToResponse(this IEnumerable<Competition> competitions)
        => [.. competitions.Select(ToResponse)];

    public static CompetitionResponse ToResponse(this Competition competition)
        => new(competition.Id,
               competition.SeasonId,
               competition.Name,
               competition.Format);
}
