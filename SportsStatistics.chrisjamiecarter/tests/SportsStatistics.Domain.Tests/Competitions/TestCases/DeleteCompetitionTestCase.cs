using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Tests.Competitions.TestData;

namespace SportsStatistics.Domain.Tests.Competitions.TestCases;

public class DeleteCompetitionTestCase : TheoryData<Competition, DateTime>
{
    private static readonly Competition Competition = CompetitionTestData.ValidCompetition;

    public DeleteCompetitionTestCase()
    {
        Add(Competition, DateTime.UtcNow);
    }
}
