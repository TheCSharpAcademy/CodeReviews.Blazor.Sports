using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed record KickoffTimeUtc
{
    private KickoffTimeUtc(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; }

    public static implicit operator DateTime(KickoffTimeUtc? kickoffTimeUtc) =>
        kickoffTimeUtc is not null ? kickoffTimeUtc.Value : throw new ArgumentNullException(nameof(kickoffTimeUtc));

    public static Result<KickoffTimeUtc> Create(DateTime? value)
    {
        if (value is null || value == DateTime.MinValue)
        {
            return FixtureErrors.KickoffTimeUtc.NullOrEmpty;
        }

        return new KickoffTimeUtc(value.Value);
    }
}
