using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class CreatePlayerTestCase : TheoryData<Name, SquadNumber, Nationality, DateOfBirth, Position>
{
    public CreatePlayerTestCase()
    {
        Add(NameTestData.ValidName,
            SquadNumberTestData.ValidSquadNumber,
            NationalityTestData.ValidNationality,
            DateOfBirthTestData.ValidDateOfBirth,
            PositionTestData.ValidPosition);
    }
}
