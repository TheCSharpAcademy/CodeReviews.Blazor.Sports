using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class NameValidTestCase : TheoryData<string, Name>
{
    public NameValidTestCase()
    {
        Add(NameTestData.ValidName.Value, NameTestData.ValidName);
    }
}
