using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class NationalityValidTestCase : TheoryData<string, Nationality>
{
    public NationalityValidTestCase()
    {
        Add(NationalityTestData.ValidNationality.Value, NationalityTestData.ValidNationality);
    }
}
