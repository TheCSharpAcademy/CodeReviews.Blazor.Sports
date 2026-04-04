using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class ChangeKickoffTimeUtcDifferentTestCase : TheoryData<Fixture, KickoffTimeUtc>
{
    private static readonly Fixture Fixture = FixtureTestData.ValidFixture;

    private static readonly KickoffTimeUtc DifferentKickoffTimeUtc = KickoffTimeUtc.Create(Fixture.KickoffTimeUtc.Value.AddDays(1)).Value;

    public ChangeKickoffTimeUtcDifferentTestCase()
    {
        Add(Fixture, DifferentKickoffTimeUtc);
    }
}
