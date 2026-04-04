using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class SquadNumberValidTestCase : TheoryData<int, SquadNumber>
{
    private static readonly SquadNumber ValidSquadNumber = SquadNumberTestData.ValidSquadNumber;
    public SquadNumberValidTestCase()
    {
        Add(ValidSquadNumber.Value, ValidSquadNumber);
    }
}
