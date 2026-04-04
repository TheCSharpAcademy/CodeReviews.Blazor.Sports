using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures.GetBySeasonId;

internal static class FixtureMapper
{
    public static FixtureResponse ToResponse(this Fixture fixture, Competition competition)
        => new(fixture.Id,
               competition.Id,
               competition.Name,
               fixture.Opponent.Value,
               fixture.KickoffTimeUtc.Value,
               fixture.Location.Value,
               fixture.Location.Name,
               fixture.Score.HomeGoals,
               fixture.Score.AwayGoals,
               fixture.Status.Value,
               fixture.Status.Name);
}
