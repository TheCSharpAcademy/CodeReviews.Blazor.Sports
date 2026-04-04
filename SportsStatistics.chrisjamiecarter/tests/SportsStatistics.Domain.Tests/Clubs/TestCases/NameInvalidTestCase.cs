using SportsStatistics.Domain.Clubs;
using SportsStatistics.SharedKernel;
using SportsStatistics.Domain.Tests.Clubs.TestData;

namespace SportsStatistics.Domain.Tests.Clubs.TestCases;

public class NameInvalidTestCase : TheoryData<string?, Error>
{
    public NameInvalidTestCase()
    {
        Add(NameTestData.NullName, ClubErrors.Name.IsRequired);
        Add(NameTestData.EmptyName, ClubErrors.Name.IsRequired);
        Add(NameTestData.WhitespaceName, ClubErrors.Name.IsRequired);
        Add(NameTestData.LongerThanAllowedName, ClubErrors.Name.ExceedsMaxLength);
    }
}
