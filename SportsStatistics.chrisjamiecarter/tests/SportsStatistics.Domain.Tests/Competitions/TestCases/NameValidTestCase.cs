using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Tests.Competitions.TestData;

namespace SportsStatistics.Domain.Tests.Competitions.TestCases;

public class NameValidTestCase : TheoryData<string, Name>
{
    public NameValidTestCase()
    {
        Add(NameTestData.ValidName.Value, NameTestData.ValidName);
    }
}
