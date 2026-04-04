using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class FixtureOpponentChangedDomainEventTestCase : TheoryData<Fixture, Opponent, FixtureOpponentChangedDomainEvent>
{
    private static readonly Fixture Fixture = FixtureTestData.ValidFixture;

    private static readonly Opponent DifferentOpponent = Opponent.Create($"{Fixture.Opponent.Value} Updated").Value;

    public FixtureOpponentChangedDomainEventTestCase()
    {
        Add(Fixture, DifferentOpponent, new(Fixture, Fixture.Opponent));
    }
}
