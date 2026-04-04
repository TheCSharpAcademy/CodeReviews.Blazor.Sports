using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Teamsheets.TestData;

public static class TeamsheetTestData
{
    public static Guid ValidFixtureId => Guid.NewGuid();

    public static Guid ValidPlayerId => Guid.NewGuid();

    public static DateTime ValidSubmittedAtUtc => new(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);

    public static DateTime ValidUtcNow => new(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);
}