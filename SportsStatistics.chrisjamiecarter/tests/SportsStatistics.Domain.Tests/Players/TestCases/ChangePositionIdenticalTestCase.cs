using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ChangePositionIdenticalTestCase : TheoryData<Player, Position>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    private static readonly Position IdenticalPosition = Position.List.Single(position => position == Player.Position);

    public ChangePositionIdenticalTestCase()
    {
        Add(Player, IdenticalPosition);
    }
}
