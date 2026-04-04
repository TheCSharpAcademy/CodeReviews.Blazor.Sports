using SportsStatistics.Domain.Clubs;
using SportsStatistics.Domain.Tests.Clubs.TestData;

namespace SportsStatistics.Domain.Tests.Clubs.TestCases;

public class ChangeNameDifferentTestCase : TheoryData<Club, Name>
{
    private static readonly Club Club = ClubTestData.ValidClub;

    private static readonly Name DifferentName = Name.Create($"{Club.Name.Value} Updated").Value;

    public ChangeNameDifferentTestCase()
    {
        Add(Club, DifferentName);
    }
}
