using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Tests.Competitions.TestData;
using SportsStatistics.Domain.Tests.Seasons.TestData;

namespace SportsStatistics.Domain.Tests.Competitions.TestCases;

public class CreateCompetitionTestCase : TheoryData<Season, Name, Format>
{
    public CreateCompetitionTestCase()
    {
        Add(SeasonTestData.ValidSeason, NameTestData.ValidName, FormatTestData.ValidFormat);
    }
}
