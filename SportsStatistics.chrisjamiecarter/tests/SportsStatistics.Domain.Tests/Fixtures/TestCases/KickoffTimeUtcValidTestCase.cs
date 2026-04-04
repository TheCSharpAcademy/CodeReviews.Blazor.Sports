using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class KickoffTimeUtcValidTestCase : TheoryData<DateTime, KickoffTimeUtc>
{
    private static readonly KickoffTimeUtc KickoffTimeUtc = KickoffTimeUtcTestData.ValidKickoffTimeUtc;

    public KickoffTimeUtcValidTestCase()
    {
        Add(KickoffTimeUtc.Value, KickoffTimeUtc);
    }
}
