using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Domain.Tests.Fixtures.TestData;

public static class KickoffTimeUtcTestData
{
    public static KickoffTimeUtc ValidKickoffTimeUtc => KickoffTimeUtc.Create(DateTime.UtcNow).Value;

    public static readonly DateTime? NullKickoffTimeUtc;

    public static readonly DateTime EmptyKickoffTimeUtc = DateTime.MinValue;
}
