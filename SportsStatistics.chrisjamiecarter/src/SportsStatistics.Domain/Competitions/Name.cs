using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed record Name
{
    public const int MaxLength = 50;

    private Name(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(Name? name) =>
        name is not null ? name.Value : throw new ArgumentNullException(nameof(name));

    public static Result<Name> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return CompetitionErrors.Name.NullOrEmpty;
        }

        if (value.Length > MaxLength)
        {
            return CompetitionErrors.Name.ExceedsMaxLength;
        }

        return new Name(value);
    }
}
