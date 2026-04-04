using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ChangeNameIdenticalTestCase : TheoryData<Player, Name>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    private static readonly Name IdenticalName = Name.Create(Player.Name.Value).Value;

    public ChangeNameIdenticalTestCase()
    {
        Add(Player, IdenticalName);
    }
}
