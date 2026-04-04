using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class SquadNumberInvalidTestCase : TheoryData<int?, Error>
{
    public SquadNumberInvalidTestCase()
    {
        Add(SquadNumberTestData.NullSquadNumber, PlayerErrors.SquadNumber.NullOrEmpty);
        Add(SquadNumberTestData.BelowMinValueSquadNumber, PlayerErrors.SquadNumber.BelowMinValue);
        Add(SquadNumberTestData.AboveMaxValueSquadNumber, PlayerErrors.SquadNumber.AboveMaxValue);
    }
}
