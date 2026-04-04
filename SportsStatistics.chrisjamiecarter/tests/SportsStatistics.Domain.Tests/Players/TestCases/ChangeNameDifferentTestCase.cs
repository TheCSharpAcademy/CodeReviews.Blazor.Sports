using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ChangeNameDifferentTestCase : TheoryData<Player, Name>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    private static readonly Name DifferentName = Name.Create($"{Player.Name.Value} Updated").Value;

    public ChangeNameDifferentTestCase()
    {
        Add(Player, DifferentName);
    }
}
