using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class NationalityInvalidTestCase : TheoryData<string?, Error>
{
    public NationalityInvalidTestCase()
    {
        Add(NationalityTestData.NullNationality, PlayerErrors.Nationality.NullOrEmpty);
        Add(NationalityTestData.EmptyNationality, PlayerErrors.Nationality.NullOrEmpty);
        Add(NationalityTestData.WhitespaceNationality, PlayerErrors.Nationality.NullOrEmpty);
        Add(NationalityTestData.LongerThanAllowedNationality, PlayerErrors.Nationality.ExceedsMaxLength);
    }
}
