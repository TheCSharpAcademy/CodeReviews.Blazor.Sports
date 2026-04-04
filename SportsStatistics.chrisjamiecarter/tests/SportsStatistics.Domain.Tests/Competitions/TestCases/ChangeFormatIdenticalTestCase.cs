using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Tests.Competitions.TestData;

namespace SportsStatistics.Domain.Tests.Competitions.TestCases;

public class ChangeFormatIdenticalTestCase : TheoryData<Competition, Format>
{
    private static readonly Competition Competition = CompetitionTestData.ValidCompetition;

    private static readonly Format IdenticalFormat = Format.List.Single(position => position == Competition.Format);

    public ChangeFormatIdenticalTestCase()
    {
        Add(Competition, IdenticalFormat);
    }
}
