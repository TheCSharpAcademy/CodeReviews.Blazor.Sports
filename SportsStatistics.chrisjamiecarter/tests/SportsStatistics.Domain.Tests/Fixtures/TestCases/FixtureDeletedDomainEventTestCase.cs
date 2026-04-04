using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class FixtureDeletedDomainEventTestCase : TheoryData<Fixture, DateTime, FixtureDeletedDomainEvent>
{
    private static readonly Fixture Fixture = FixtureTestData.ValidFixture;

    public FixtureDeletedDomainEventTestCase()
    {
        Add(Fixture, DateTime.UtcNow, new(Fixture.Id));
    }
}
