using SportsStatistics.Domain.Clubs;
using SportsStatistics.Domain.Tests.Clubs.TestData;

namespace SportsStatistics.Domain.Tests.Clubs.TestCases;

public class ClubNameChangedDomainEventTestCase : TheoryData<Club, Name, ClubNameChangedDomainEvent>
{
    private static readonly Club Club = ClubTestData.ValidClub;

    private static readonly Name DifferentName = Name.Create($"{Club.Name.Value} Updated").Value;

    public ClubNameChangedDomainEventTestCase()
    {
        Add(Club, DifferentName, new(Club, Club.Name));
    }
}
