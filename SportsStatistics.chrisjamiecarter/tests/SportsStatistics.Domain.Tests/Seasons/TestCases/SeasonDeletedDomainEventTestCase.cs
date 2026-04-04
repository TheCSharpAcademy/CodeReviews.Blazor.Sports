using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Seasons.TestData;

namespace SportsStatistics.Domain.Tests.Seasons.TestCases;

public class SeasonDeletedDomainEventTestCase : TheoryData<Season, DateTime, SeasonDeletedDomainEvent>
{
    private static readonly Season Season = SeasonTestData.ValidSeason;

    public SeasonDeletedDomainEventTestCase()
    {
        Add(Season, DateTime.UtcNow, new(Season.Id));
    }
}
