using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class ChangeLocationDifferentTestCase : TheoryData<Fixture, Location>
{
    private static readonly Fixture Fixture = FixtureTestData.ValidFixture;

    private static readonly Location DifferentLocation = Location.List.First(location => location != Fixture.Location);

    public ChangeLocationDifferentTestCase()
    {
        Add(Fixture, DifferentLocation);
    }
}
