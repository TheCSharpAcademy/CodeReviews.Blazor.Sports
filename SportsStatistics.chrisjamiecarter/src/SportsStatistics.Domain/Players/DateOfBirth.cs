using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed record DateOfBirth
{
    public const int MinAge = 15;

    private DateOfBirth(DateOnly value)
    {
        Value = value;
    }

    public DateOnly Value { get; }

    public static implicit operator DateOnly(DateOfBirth? dateOfBirth) =>
        dateOfBirth is not null ? dateOfBirth.Value : throw new ArgumentNullException(nameof(dateOfBirth));

    public static Result<DateOfBirth> Create(DateOnly? value)
    {
        if (value is null || value == DateOnly.MinValue)
        {
            return PlayerErrors.DateOfBirth.NullOrEmpty;
        }

        if (value > DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-MinAge)))
        {
            return PlayerErrors.DateOfBirthBelowMinAge;
        }

        return new DateOfBirth(value.Value);
    }
}
