using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Teamsheets;

public sealed class TeamsheetPlayer : Entity
{
    private TeamsheetPlayer(Guid teamsheetId,
                            Guid playerId,
                            bool isStarter)
        : base(Guid.CreateVersion7())
    {
        TeamsheetId = teamsheetId;
        PlayerId = playerId;
        IsStarter = isStarter;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="TeamsheetPlayer"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    private TeamsheetPlayer() { }

    public Guid TeamsheetId { get; private set; }

    public Guid PlayerId { get; private set; }

    public bool IsStarter { get; private set; }

    public static TeamsheetPlayer CreateStarter(Guid teamsheetId, Guid playerId)
    {
        return new TeamsheetPlayer(teamsheetId, playerId, true);
    }

    public static TeamsheetPlayer CreateSubstitute(Guid teamsheetId, Guid playerId)
    {
        return new TeamsheetPlayer(teamsheetId, playerId, false);
    }
}
