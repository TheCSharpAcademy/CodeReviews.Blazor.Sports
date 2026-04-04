using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class FixtureKickoffTimeUtcChangedDomainEventTestCase : TheoryData<Fixture, KickoffTimeUtc, FixtureKickoffTimeUtcChangedDomainEvent>
{
    private static readonly Fixture Fixture = FixtureTestData.ValidFixture;

    private static readonly KickoffTimeUtc DifferentKickoffTimeUtc = KickoffTimeUtc.Create(Fixture.KickoffTimeUtc.Value.AddDays(1)).Value;

    public FixtureKickoffTimeUtcChangedDomainEventTestCase()
    {
        Add(Fixture, DifferentKickoffTimeUtc, new(Fixture, Fixture.KickoffTimeUtc));
    }
}
