using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class ChangeOpponentIdenticalTestCase : TheoryData<Fixture, Opponent>
{
    private static readonly Fixture Fixture = FixtureTestData.ValidFixture;

    private static readonly Opponent IdenticalOpponent = Opponent.Create(Fixture.Opponent.Value).Value;

    public ChangeOpponentIdenticalTestCase()
    {
        Add(Fixture, IdenticalOpponent);
    }
}
