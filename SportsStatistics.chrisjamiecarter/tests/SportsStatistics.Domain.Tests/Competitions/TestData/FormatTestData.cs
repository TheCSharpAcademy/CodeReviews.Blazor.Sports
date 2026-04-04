using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Domain.Tests.Competitions.TestData;

public static class FormatTestData
{
    public static Format ValidFormat => Format.List.OrderBy(_ => Random.Shared.Next()).First();
}
