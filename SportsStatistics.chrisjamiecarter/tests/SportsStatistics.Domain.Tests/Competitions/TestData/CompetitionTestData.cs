using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Tests.Seasons.TestData;

namespace SportsStatistics.Domain.Tests.Competitions.TestData;

public static class CompetitionTestData
{
    public static Competition ValidCompetition => Competition.Create(
        SeasonTestData.ValidSeason,
        NameTestData.ValidName,
        FormatTestData.ValidFormat);
}
