using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class DateOfBirthInvalidTestCase : TheoryData<DateOnly?, Error>
{
    public DateOfBirthInvalidTestCase()
    {
        Add(DateOfBirthTestData.NullDateOfBirth, PlayerErrors.DateOfBirth.NullOrEmpty);
        Add(DateOfBirthTestData.EmptyDateOfBirth, PlayerErrors.DateOfBirth.NullOrEmpty);
        Add(DateOfBirthTestData.YoungerThanAllowedDateOfBirth, PlayerErrors.DateOfBirthBelowMinAge);
    }
}
