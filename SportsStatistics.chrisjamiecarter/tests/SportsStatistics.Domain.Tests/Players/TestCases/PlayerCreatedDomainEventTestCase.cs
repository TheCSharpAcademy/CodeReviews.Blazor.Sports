using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class PlayerCreatedDomainEventTestCase : TheoryData<Name, SquadNumber, Nationality, DateOfBirth, Position>
{
    public PlayerCreatedDomainEventTestCase()
    {
        Add(NameTestData.ValidName,
            SquadNumberTestData.ValidSquadNumber,
            NationalityTestData.ValidNationality,
            DateOfBirthTestData.ValidDateOfBirth,
            PositionTestData.ValidPosition);
    }
}
