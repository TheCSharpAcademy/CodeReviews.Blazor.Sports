using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Tests.Fixtures.TestData;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Tests.Fixtures.TestCases;

public class KickoffTimeUtcInvalidTestCase : TheoryData<DateTime?, Error>
{
    public KickoffTimeUtcInvalidTestCase()
    {
        Add(KickoffTimeUtcTestData.NullKickoffTimeUtc, FixtureErrors.KickoffTimeUtc.NullOrEmpty);
        Add(KickoffTimeUtcTestData.EmptyKickoffTimeUtc, FixtureErrors.KickoffTimeUtc.NullOrEmpty);
    }
}
