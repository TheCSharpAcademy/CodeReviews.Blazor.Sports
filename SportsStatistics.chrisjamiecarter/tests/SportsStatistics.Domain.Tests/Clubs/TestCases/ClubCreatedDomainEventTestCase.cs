using SportsStatistics.Domain.Clubs;
using SportsStatistics.Domain.Tests.Clubs.TestData;

namespace SportsStatistics.Domain.Tests.Clubs.TestCases;

public class ClubCreatedDomainEventTestCase : TheoryData<Name>
{
    public ClubCreatedDomainEventTestCase()
    {
        Add(NameTestData.ValidName);
    }
}