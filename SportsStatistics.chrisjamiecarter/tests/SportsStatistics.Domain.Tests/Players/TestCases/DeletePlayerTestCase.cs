using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class DeletePlayerTestCase : TheoryData<Player, DateTime>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    public DeletePlayerTestCase()
    {
        Add(Player, DateTime.UtcNow);
    }
}
