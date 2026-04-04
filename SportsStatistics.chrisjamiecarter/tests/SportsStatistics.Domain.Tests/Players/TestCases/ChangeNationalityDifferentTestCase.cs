using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ChangeNationalityDifferentTestCase : TheoryData<Player, Nationality>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    private static readonly Nationality DifferentNationality = Nationality.Create($"{Player.Nationality.Value} Updated").Value;

    public ChangeNationalityDifferentTestCase()
    {
        Add(Player, DifferentNationality);
    }
}
