using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed record SquadNumber
{
    public const int MinValue = 1;
    public const int MaxValue = 99;

    private SquadNumber(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static implicit operator int(SquadNumber? squadNumber) =>
        squadNumber is not null ? squadNumber.Value : throw new ArgumentNullException(nameof(squadNumber));

    public static Result<SquadNumber> Create(int? value)
    {
        if (value is null)
        {
            return PlayerErrors.SquadNumber.NullOrEmpty;
        }

        if (value < MinValue)
        {
            return PlayerErrors.SquadNumber.BelowMinValue;
        }

        if (value > MaxValue)
        {
            return PlayerErrors.SquadNumber.AboveMaxValue;
        }

        return new SquadNumber(value.Value);
    }
}
