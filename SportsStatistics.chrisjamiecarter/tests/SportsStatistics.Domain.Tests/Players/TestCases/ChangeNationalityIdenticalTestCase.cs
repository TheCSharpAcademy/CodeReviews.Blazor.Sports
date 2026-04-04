using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ChangeNationalityIdenticalTestCase : TheoryData<Player, Nationality>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    private static readonly Nationality IdenticalNationality = Nationality.Create(Player.Nationality.Value).Value;

    public ChangeNationalityIdenticalTestCase()
    {
        Add(Player, IdenticalNationality);
    }
}
