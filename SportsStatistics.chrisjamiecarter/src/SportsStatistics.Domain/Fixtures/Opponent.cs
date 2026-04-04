using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed record Opponent
{
    public const int MaxLength = 100;

    private Opponent(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(Opponent? opponent) =>
        opponent is not null ? opponent.Value : throw new ArgumentNullException(nameof(opponent));

    public static Result<Opponent> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return FixtureErrors.Opponent.NullOrEmpty;
        }

        if (value.Length > MaxLength)
        {
            return FixtureErrors.Opponent.ExceedsMaxLength;
        }

        return new Opponent(value);
    }
}
