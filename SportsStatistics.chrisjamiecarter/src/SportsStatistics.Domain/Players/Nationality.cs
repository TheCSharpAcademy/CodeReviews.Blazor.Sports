using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed record Nationality
{
    public const int MaxLength = 100;

    private Nationality(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(Nationality? nationality) =>
        nationality is not null ? nationality.Value : throw new ArgumentNullException(nameof(nationality));

    public static Result<Nationality> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return PlayerErrors.Nationality.NullOrEmpty;
        }

        if (value.Length > MaxLength)
        {
            return PlayerErrors.Nationality.ExceedsMaxLength;
        }

        return new Nationality(value);
    }
}
