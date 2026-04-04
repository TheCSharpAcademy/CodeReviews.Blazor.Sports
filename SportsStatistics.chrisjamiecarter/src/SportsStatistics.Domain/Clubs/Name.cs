using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Clubs;

public sealed record Name
{
    public const string DefaultValue = "Sports Statistics FC";
    public const int MaxLength = 100;

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
            return ClubErrors.Name.IsRequired;
        }

        if (value.Length > MaxLength)
        {
            return ClubErrors.Name.ExceedsMaxLength;
        }

        return new Name(value);
    }
}
