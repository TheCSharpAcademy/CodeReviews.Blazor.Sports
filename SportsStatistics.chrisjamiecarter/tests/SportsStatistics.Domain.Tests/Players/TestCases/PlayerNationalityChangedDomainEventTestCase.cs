using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class PlayerNationalityChangedDomainEventTestCase : TheoryData<Player, Nationality, PlayerNationalityChangedDomainEvent>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    private static readonly Nationality DifferentNationality = Nationality.Create($"{Player.Nationality.Value} Updated").Value;

    public PlayerNationalityChangedDomainEventTestCase()
    {
        Add(Player, DifferentNationality, new(Player, Player.Nationality));
    }
}
