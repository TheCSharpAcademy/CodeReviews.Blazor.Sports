using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class OpponentInvalidTestCase : TheoryData<string?, Error>
{
    public OpponentInvalidTestCase()
    {
        Add(OpponentTestData.NullOpponent, FixtureErrors.Opponent.NullOrEmpty);
        Add(OpponentTestData.EmptyOpponent, FixtureErrors.Opponent.NullOrEmpty);
        Add(OpponentTestData.WhitespaceOpponent, FixtureErrors.Opponent.NullOrEmpty);
        Add(OpponentTestData.LongerThanAllowedOpponent, FixtureErrors.Opponent.ExceedsMaxLength);
    }
}
