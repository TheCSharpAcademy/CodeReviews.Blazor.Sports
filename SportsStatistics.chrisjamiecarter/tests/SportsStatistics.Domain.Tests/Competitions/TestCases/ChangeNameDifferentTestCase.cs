using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Tests.Competitions.TestData;

namespace SportsStatistics.Domain.Tests.Competitions.TestCases;

public class ChangeNameDifferentTestCase : TheoryData<Competition, Name>
{
    private static readonly Competition Competition = CompetitionTestData.ValidCompetition;

    private static readonly Name DifferentName = Name.Create($"{Competition.Name.Value} Updated").Value;

    public ChangeNameDifferentTestCase()
    {
        Add(Competition, DifferentName);
    }
}
