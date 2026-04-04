using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Tests.Competitions.TestData;

namespace SportsStatistics.Domain.Tests.Competitions.TestCases;

public class CompetitionDeletedDomainEventTestCase : TheoryData<Competition, DateTime, CompetitionDeletedDomainEvent>
{
    private static readonly Competition Competition = CompetitionTestData.ValidCompetition;

    public CompetitionDeletedDomainEventTestCase()
    {
        Add(Competition, DateTime.UtcNow, new(Competition.Id));
    }
}
