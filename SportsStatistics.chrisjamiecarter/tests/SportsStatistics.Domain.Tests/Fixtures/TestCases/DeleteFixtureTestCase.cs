using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class DeleteFixtureTestCase : TheoryData<Fixture, DateTime>
{
    private static readonly Fixture Fixture = FixtureTestData.ValidFixture;

    public DeleteFixtureTestCase()
    {
        Add(Fixture, DateTime.UtcNow);
    }
}
