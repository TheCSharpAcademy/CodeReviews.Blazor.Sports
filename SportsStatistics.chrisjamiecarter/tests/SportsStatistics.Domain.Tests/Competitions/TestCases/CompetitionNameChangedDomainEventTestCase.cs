using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Tests.Competitions.TestData;

namespace SportsStatistics.Domain.Tests.Competitions.TestCases;

public class CompetitionNameChangedDomainEventTestCase : TheoryData<Competition, Name, CompetitionNameChangedDomainEvent>
{
    private static readonly Competition Competition = CompetitionTestData.ValidCompetition;

    private static readonly Name DifferentName = Name.Create($"{Competition.Name.Value} Updated").Value;

    public CompetitionNameChangedDomainEventTestCase()
    {
        Add(Competition, DifferentName, new(Competition, Competition.Name));
    }
}
