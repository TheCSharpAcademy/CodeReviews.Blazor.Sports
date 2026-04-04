using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed class Position : Enumeration<Position>
{
    public static readonly Position Goalkeeper = new(0, nameof(Goalkeeper));
    public static readonly Position Defender = new(1, nameof(Defender));
    public static readonly Position Midfielder = new(2, nameof(Midfielder));
    public static readonly Position Attacker = new(3, nameof(Attacker));

    private Position(int id, string name) : base(id, name) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Position"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Position() { }
}
