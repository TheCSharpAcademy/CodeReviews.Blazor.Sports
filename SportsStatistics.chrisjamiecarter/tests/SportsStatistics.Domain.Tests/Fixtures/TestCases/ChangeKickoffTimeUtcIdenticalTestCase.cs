using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class ChangeKickoffTimeUtcIdenticalTestCase : TheoryData<Fixture, KickoffTimeUtc>
{
    private static readonly Fixture Fixture = FixtureTestData.ValidFixture;

    private static readonly KickoffTimeUtc IdenticalKickoffTimeUtc = KickoffTimeUtc.Create(Fixture.KickoffTimeUtc.Value).Value;

    public ChangeKickoffTimeUtcIdenticalTestCase()
    {
        Add(Fixture, IdenticalKickoffTimeUtc);
    }
}
