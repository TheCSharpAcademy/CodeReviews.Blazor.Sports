using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Tests.Competitions.TestData;

namespace SportsStatistics.Domain.Tests.Competitions.TestCases;

public class ChangeNameIdenticalTestCase : TheoryData<Competition, Name>
{
    private static readonly Competition Competition = CompetitionTestData.ValidCompetition;

    private static readonly Name IdenticalName = Name.Create(Competition.Name.Value).Value;

    public ChangeNameIdenticalTestCase()
    {
        Add(Competition, IdenticalName);
    }
}
