using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Players.TestData;

public static class PositionTestData
{
    public static Position ValidPosition => Position.List.OrderBy(_ => Random.Shared.Next()).First();
}
