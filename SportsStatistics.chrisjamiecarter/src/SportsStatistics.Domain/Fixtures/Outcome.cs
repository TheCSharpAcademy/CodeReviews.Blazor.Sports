using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class Outcome : Enumeration<Outcome>
{
    public static readonly Outcome None = new(-1, nameof(None));
    public static readonly Outcome Win = new(0, nameof(Win));
    public static readonly Outcome Draw = new(1, nameof(Draw));
    public static readonly Outcome Loss = new(2, nameof(Loss));

    private Outcome(int id, string name) : base(id, name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Outcome"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Outcome() { }
}
