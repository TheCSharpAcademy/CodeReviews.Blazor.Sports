using SportsStatistics.Domain.Clubs;
using SportsStatistics.Domain.Tests.Clubs.TestData;

namespace SportsStatistics.Domain.Tests.Clubs.TestCases;

public class ChangeNameIdenticalTestCase : TheoryData<Club, Name>
{
    private static readonly Club Club = ClubTestData.ValidClub;

    private static readonly Name IdenticalName = Name.Create(Club.Name.Value).Value;

    public ChangeNameIdenticalTestCase()
    {
        Add(Club, IdenticalName);
    }
}
