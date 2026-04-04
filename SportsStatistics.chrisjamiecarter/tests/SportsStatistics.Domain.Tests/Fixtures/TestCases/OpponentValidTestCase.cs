using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class OpponentValidTestCase : TheoryData<string, Opponent>
{
    private static readonly Opponent Opponent = OpponentTestData.ValidOpponent;

    public OpponentValidTestCase()
    {
        Add(Opponent.Value, Opponent);
    }
}
