using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed class Format : Enumeration<Format>
{
    public static readonly Format League = new(0, nameof(League));
    public static readonly Format Cup = new(1, nameof(Cup));

    private Format(int id, string name) : base(id, name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Format"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Format() { }
}
