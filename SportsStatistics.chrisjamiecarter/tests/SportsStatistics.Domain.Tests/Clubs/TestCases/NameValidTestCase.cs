using SportsStatistics.Domain.Clubs;
using SportsStatistics.Domain.Tests.Clubs.TestData;

namespace SportsStatistics.Domain.Tests.Clubs.TestCases;

public class NameValidTestCase : TheoryData<string, Name>
{
    public NameValidTestCase()
    {
        Add(NameTestData.ValidName.Value, NameTestData.ValidName);
    }
}
