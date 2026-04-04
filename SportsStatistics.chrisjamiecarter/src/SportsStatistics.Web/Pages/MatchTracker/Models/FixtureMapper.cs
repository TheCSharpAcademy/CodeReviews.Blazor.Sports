using SportsStatistics.Application.Fixtures.GetByDate;

namespace SportsStatistics.Web.Pages.MatchTracker.Models;

internal static class FixtureMapper
{
    public static FixtureDto ToDto(this FixtureResponse fixture)
        => new(fixture.Id,
               fixture.CompetitionId,
               fixture.CompetitionName,
               fixture.Opponent,
               fixture.KickoffTimeUtc,
               fixture.LocationId,
               fixture.Location,
               fixture.HomeGoals,
               fixture.AwayGoals,
               fixture.StatusId,
               fixture.Status);
}
