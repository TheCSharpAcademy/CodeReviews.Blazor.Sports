using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ChangeSquadNumberIdenticalTestCase : TheoryData<Player, SquadNumber>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    private static readonly SquadNumber IdenticalSquadNumber = SquadNumber.Create(Player.SquadNumber.Value).Value;

    public ChangeSquadNumberIdenticalTestCase()
    {
        Add(Player, IdenticalSquadNumber);
    }
}
