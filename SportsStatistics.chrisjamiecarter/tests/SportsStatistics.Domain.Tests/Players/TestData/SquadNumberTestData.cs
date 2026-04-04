using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Players.TestData;

public class SquadNumberTestData
{
    public static SquadNumber ValidSquadNumber => SquadNumber.Create(Random.Shared.Next(SquadNumber.MinValue, SquadNumber.MaxValue + 1)).Value;

    public static readonly int BelowMinValueSquadNumber = SquadNumber.MinValue - 1;

    public static readonly int AboveMaxValueSquadNumber = SquadNumber.MaxValue + 1;

    public static readonly int? NullSquadNumber;
}
