using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ChangePositionDifferentTestCase : TheoryData<Player, Position>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    private static readonly Position DifferentPosition = Position.List.First(position => position != Player.Position);

    public ChangePositionDifferentTestCase()
    {
        Add(Player, DifferentPosition);
    }
}
