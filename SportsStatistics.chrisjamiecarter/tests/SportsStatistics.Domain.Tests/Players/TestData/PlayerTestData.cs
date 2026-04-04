using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Players.TestData;

public static class PlayerTestData
{
    public static Player ValidPlayer => Player.Create(
        NameTestData.ValidName,
        SquadNumberTestData.ValidSquadNumber,
        NationalityTestData.ValidNationality,
        DateOfBirthTestData.ValidDateOfBirth,
        PositionTestData.ValidPosition);
}
