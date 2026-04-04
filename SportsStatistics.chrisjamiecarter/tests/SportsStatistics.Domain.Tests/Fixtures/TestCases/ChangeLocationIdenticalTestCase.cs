using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class ChangeLocationIdenticalTestCase : TheoryData<Fixture, Location>
{
    private static readonly Fixture Fixture = FixtureTestData.ValidFixture;

    private static readonly Location IdenticalLocation = Location.List.First(location => location == Fixture.Location);

    public ChangeLocationIdenticalTestCase()
    {
        Add(Fixture, IdenticalLocation);
    }
}
