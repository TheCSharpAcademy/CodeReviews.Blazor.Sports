using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class ChangeDateOfBirthDifferentTestCase : TheoryData<Player, DateOfBirth>
{
    private static readonly Player Player = PlayerTestData.ValidPlayer;

    private static readonly DateOfBirth DifferentDateOfBirth = DateOfBirth.Create(Player.DateOfBirth.Value.AddMonths(-1)).Value;

    public ChangeDateOfBirthDifferentTestCase()
    {
        Add(Player, DifferentDateOfBirth);
    }
}
