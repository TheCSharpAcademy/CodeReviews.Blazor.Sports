using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Competitions.TestData;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class FixtureCreatedDomainEventTestCase : TheoryData<Competition, Opponent, KickoffTimeUtc, Location>
{
    public FixtureCreatedDomainEventTestCase()
    {
        Add(CompetitionTestData.ValidCompetition,
            OpponentTestData.ValidOpponent,
            KickoffTimeUtcTestData.ValidKickoffTimeUtc,
            LocationTestData.ValidLocation);
    }
}
