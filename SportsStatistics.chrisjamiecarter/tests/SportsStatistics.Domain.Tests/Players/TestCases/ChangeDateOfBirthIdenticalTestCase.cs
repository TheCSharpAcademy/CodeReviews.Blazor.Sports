using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ChangeDateOfBirthIdenticalTestCase : TheoryData<Player, DateOfBirth>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    private static readonly DateOfBirth IdenticalDateOfBirth = DateOfBirth.Create(Player.DateOfBirth.Value).Value;

    public ChangeDateOfBirthIdenticalTestCase()
    {
        Add(Player, IdenticalDateOfBirth);
    }
}
