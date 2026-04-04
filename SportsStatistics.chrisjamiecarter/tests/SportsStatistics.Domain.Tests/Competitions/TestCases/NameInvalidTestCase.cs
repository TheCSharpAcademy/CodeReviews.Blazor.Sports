using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Tests.Competitions.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Competitions.TestCases;

public class NameInvalidTestCase : TheoryData<string?, Error>
{
    public NameInvalidTestCase()
    {
        Add(NameTestData.NullName, CompetitionErrors.Name.NullOrEmpty);
        Add(NameTestData.EmptyName, CompetitionErrors.Name.NullOrEmpty);
        Add(NameTestData.WhitespaceName, CompetitionErrors.Name.NullOrEmpty);
        Add(NameTestData.LongerThanAllowedName, CompetitionErrors.Name.ExceedsMaxLength);
    }
}
