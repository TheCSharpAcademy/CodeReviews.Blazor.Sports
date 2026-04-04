using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Tests.Players.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Players.TestCases;

public class NameInvalidTestCase : TheoryData<string?, Error>
{
    public NameInvalidTestCase()
    {
        Add(NameTestData.NullName, PlayerErrors.Name.NullOrEmpty);
        Add(NameTestData.EmptyName, PlayerErrors.Name.NullOrEmpty);
        Add(NameTestData.WhitespaceName, PlayerErrors.Name.NullOrEmpty);
        Add(NameTestData.LongerThanAllowedName, PlayerErrors.Name.ExceedsMaxLength);
    }
}
