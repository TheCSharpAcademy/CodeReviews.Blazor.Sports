using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Domain.Tests.Fixtures.TestData;

public static class LocationTestData
{
    public static Location ValidLocation => Location.List.OrderBy(_ => Random.Shared.Next()).First();
}
