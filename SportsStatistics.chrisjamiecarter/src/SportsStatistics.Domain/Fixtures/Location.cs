using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class Location : Enumeration<Location>
{
    public static readonly Location Home = new(0, nameof(Home));
    public static readonly Location Away = new(1, nameof(Away));
    public static readonly Location Neutral = new(2, nameof(Neutral));

    private Location(int id, string name) : base(id, name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Location"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Location() { }
}
