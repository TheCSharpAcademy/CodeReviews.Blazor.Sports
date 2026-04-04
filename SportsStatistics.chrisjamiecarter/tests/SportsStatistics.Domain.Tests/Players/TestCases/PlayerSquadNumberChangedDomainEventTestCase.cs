using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class PlayerSquadNumberChangedDomainEventTestCase : TheoryData<Player, SquadNumber, PlayerSquadNumberChangedDomainEvent>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    private static readonly SquadNumber DifferentSquadNumber = SquadNumber.Create(Player.SquadNumber.Value + 1).Value;

    public PlayerSquadNumberChangedDomainEventTestCase()
    {
        Add(Player, DifferentSquadNumber, new(Player, Player.SquadNumber));
    }
}
