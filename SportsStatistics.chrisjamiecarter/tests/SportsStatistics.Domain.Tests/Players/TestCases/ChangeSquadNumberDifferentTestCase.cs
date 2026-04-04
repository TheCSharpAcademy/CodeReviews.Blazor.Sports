using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ChangeSquadNumberDifferentTestCase : TheoryData<Player, SquadNumber>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    private static readonly SquadNumber DifferentSquadNumber = SquadNumber.Create(Player.SquadNumber.Value + 1).Value;

    public ChangeSquadNumberDifferentTestCase()
    {
        Add(Player, DifferentSquadNumber);
    }
}
