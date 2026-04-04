using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Players.TestData;

public class NameTestData
{
    public static Name ValidName => Name.Create(nameof(Name)).Value;

    public static readonly string LongerThanAllowedName = new('*', Name.MaxLength + 1);

    public static readonly string? NullName;

    public static readonly string EmptyName = string.Empty;

    public static readonly string WhitespaceName = "    ";
}
