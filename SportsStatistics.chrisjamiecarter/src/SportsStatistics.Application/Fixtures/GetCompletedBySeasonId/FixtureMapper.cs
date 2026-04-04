using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures.GetCompletedBySeasonId;

internal static class FixtureMapper
{
    public static FixtureResponse ToResponse(this Fixture fixture, Competition competition) => new(
        fixture.Id,
        competition.Id,
        competition.Name,
        fixture.Opponent.Value,
        fixture.KickoffTimeUtc.Value,
        fixture.Location,
        fixture.Score,
        fixture.Status,
        fixture.Outcome);
}
