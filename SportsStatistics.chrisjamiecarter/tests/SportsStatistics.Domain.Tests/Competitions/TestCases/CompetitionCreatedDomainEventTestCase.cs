using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Competitions.TestData;
using SportsStatistics.Domain.Tests.Seasons.TestData;

namespace SportsStatistics.Domain.Tests.Competitions.TestCases;

public class CompetitionCreatedDomainEventTestCase : TheoryData<Season, Name, Format>
{
    public CompetitionCreatedDomainEventTestCase()
    {
        Add(SeasonTestData.ValidSeason, NameTestData.ValidName, FormatTestData.ValidFormat);
    }
}
