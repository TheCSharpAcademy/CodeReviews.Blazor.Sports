using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Competitions.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestData;

public static class FixtureTestData
{
    public static Fixture ValidFixture => Fixture.Create(
        CompetitionTestData.ValidCompetition,
        OpponentTestData.ValidOpponent,
        KickoffTimeUtcTestData.ValidKickoffTimeUtc,
        LocationTestData.ValidLocation);
}
