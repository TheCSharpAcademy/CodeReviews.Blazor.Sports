using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class ChangeOpponentDifferentTestCase : TheoryData<Fixture, Opponent>
{
    private static readonly Fixture Fixture = FixtureTestData.ValidFixture;

    private static readonly Opponent DifferentOpponent = Opponent.Create($"{Fixture.Opponent.Value} Updated").Value;

    public ChangeOpponentDifferentTestCase()
    {
        Add(Fixture, DifferentOpponent);
    }
}
