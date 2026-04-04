using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class FixtureLocationChangedDomainEventTestCase : TheoryData<Fixture, Location, FixtureLocationChangedDomainEvent>
{
    private static readonly Fixture Fixture = FixtureTestData.ValidFixture;

    private static readonly Location DifferentLocation = Location.List.First(location => location != Fixture.Location);

    public FixtureLocationChangedDomainEventTestCase()
    {
        Add(Fixture, DifferentLocation, new(Fixture, Fixture.Location));
    }
}
