using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class DateOfBirthValidTestCase : TheoryData<DateOnly, DateOfBirth>
{
    public DateOfBirthValidTestCase()
    {
        Add(DateOfBirthTestData.ValidDateOfBirth.Value, DateOfBirthTestData.ValidDateOfBirth);
    }
}
