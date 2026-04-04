using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Tests.Competitions.TestData;

namespace SportsStatistics.Domain.Tests.Competitions.TestCases;

public class ChangeFormatDifferentTestCase : TheoryData<Competition, Format>
{
    private static readonly Competition Competition = CompetitionTestData.ValidCompetition;

    private static readonly Format DifferentFormat = Format.List.First(format => format != Competition.Format);

    public ChangeFormatDifferentTestCase()
    {
        Add(Competition, DifferentFormat);
    }
}
