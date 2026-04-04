using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Players.TestData;

public static class NationalityTestData
{
    public static Nationality ValidNationality => Nationality.Create(nameof(Nationality)).Value;
    
    public static readonly string LongerThanAllowedNationality = new('*', Nationality.MaxLength + 1);

    public static readonly string? NullNationality;

    public static readonly string EmptyNationality = string.Empty;

    public static readonly string WhitespaceNationality = "    ";
}
