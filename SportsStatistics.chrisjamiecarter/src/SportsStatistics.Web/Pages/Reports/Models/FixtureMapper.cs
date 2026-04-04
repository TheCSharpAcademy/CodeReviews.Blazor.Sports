using SportsStatistics.Application.Fixtures.GetCompletedBySeasonId;

namespace SportsStatistics.Web.Pages.Reports.Models;

internal static class FixtureMapper
{
    public static IQueryable<FixtureDto> ToQueryable(this List<FixtureResponse> fixtures, string clubName) =>
        fixtures.Select(fixture => fixture.ToDto(clubName)).AsQueryable();

    public static FixtureDto ToDto(this FixtureResponse fixture, string clubName) => new(
        fixture.Id,
        fixture.CompetitionId,
        fixture.CompetitionName,
        clubName,
        fixture.Opponent,
        fixture.KickoffTimeUtc,
        fixture.Location,
        fixture.Score,
        fixture.Status,
        fixture.Outcome);
}
