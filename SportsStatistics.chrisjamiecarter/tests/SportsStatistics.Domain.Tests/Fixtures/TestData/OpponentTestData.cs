using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Domain.Tests.Fixtures.TestData;

public static class OpponentTestData
{
    public static Opponent ValidOpponent => Opponent.Create(nameof(Opponent)).Value;

    public static readonly string LongerThanAllowedOpponent = new('*', Opponent.MaxLength + 1);

    public static readonly string? NullOpponent;

    public static readonly string EmptyOpponent = string.Empty;

    public static readonly string WhitespaceOpponent = "    ";
}
