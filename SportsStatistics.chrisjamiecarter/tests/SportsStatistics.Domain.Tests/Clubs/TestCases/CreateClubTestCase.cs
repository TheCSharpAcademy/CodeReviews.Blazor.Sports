using SportsStatistics.Domain.Clubs;
using SportsStatistics.Domain.Tests.Clubs.TestData;

namespace SportsStatistics.Domain.Tests.Clubs.TestCases;

public class CreateClubTestCase : TheoryData<Name>
{
    public CreateClubTestCase()
    {
        Add(NameTestData.ValidName);
    }
}
