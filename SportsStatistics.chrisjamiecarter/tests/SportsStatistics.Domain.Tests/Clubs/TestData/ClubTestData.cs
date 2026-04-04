using SportsStatistics.Domain.Clubs;

namespace SportsStatistics.Domain.Tests.Clubs.TestData;

public static class ClubTestData
{
    public static Club ValidClub => Club.Create(NameTestData.ValidName);
}
