using SportsStatistics.Domain.Players;

namespace SportsStatistics.Domain.Tests.Players.TestData;

public class DateOfBirthTestData
{
    public static DateOfBirth ValidDateOfBirth => DateOfBirth.Create(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-DateOfBirth.MinAge - 1))).Value;

    public static readonly DateOnly YoungerThanAllowedDateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-DateOfBirth.MinAge + 1));

    public static readonly DateOnly EmptyDateOfBirth = DateOnly.MinValue;

    public static readonly DateOnly? NullDateOfBirth;
}
