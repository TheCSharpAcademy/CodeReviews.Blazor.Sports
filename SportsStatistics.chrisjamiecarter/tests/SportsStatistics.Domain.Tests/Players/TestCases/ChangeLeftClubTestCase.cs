using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ChangeLeftClubTestCase : TheoryData<Player>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    public ChangeLeftClubTestCase()
    {
        Add(Player);
    }
}