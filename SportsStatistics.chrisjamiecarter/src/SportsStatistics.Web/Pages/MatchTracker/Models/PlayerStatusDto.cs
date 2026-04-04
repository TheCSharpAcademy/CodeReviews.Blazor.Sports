namespace SportsStatistics.Web.Pages.MatchTracker.Models;

/// <summary>
/// Represents the status of a player during a match, including cards, goals, assists, and substitution.
/// </summary>
/// <param name="PlayerId">The unique identifier of the player.</param>
/// <param name="HasYellowCard">Whether the player has received a yellow card.</param>
/// <param name="HasRedCard">Whether the player has received a red card.</param>
/// <param name="Goals">The list of goals scored by the player.</param>
/// <param name="Assists">The list of assists made by the player.</param>
/// <param name="Substitution">The substitution status, if applicable.</param>
public sealed record PlayerStatusDto(
    Guid PlayerId,
    bool HasYellowCard,
    bool HasRedCard,
    IReadOnlyList<GoalEventDto> Goals,
    IReadOnlyList<AssistEventDto> Assists,
    IReadOnlyList<OwnGoalEventDto> OwnGoals,
    SubstitutionStatusDto? Substitution)
{
    /// <summary>
    /// Gets a value indicating whether the player has scored any goals.
    /// </summary>
    public bool HasGoals => Goals.Count > 0;

    /// <summary>
    /// Gets a value indicating whether the player has made any assists.
    /// </summary>
    public bool HasAssists => Assists.Count > 0;

    /// <summary>
    /// Gets a value indicating whether the player has scored any own goals.
    /// </summary>
    public bool HasOwnGoals => OwnGoals.Count > 0;

    /// <summary>
    /// Gets a value indicating whether the player has been sent off (received a red card).
    /// </summary>
    public bool IsSentOff => HasRedCard;

    /// <summary>
    /// Gets a value indicating whether the player has any status to display.
    /// </summary>
    public bool HasAnyStatus => HasYellowCard || HasRedCard || HasGoals || HasAssists || HasOwnGoals || Substitution is not null;
}

/// <summary>
/// Represents a goal event for a player.
/// </summary>
/// <param name="OccurredAtUtc">The UTC timestamp when the goal was scored.</param>
public sealed record GoalEventDto(DateTime OccurredAtUtc);

/// <summary>
/// Represents an assist event for a player.
/// </summary>
/// <param name="OccurredAtUtc">The UTC timestamp when the assist was made.</param>
public sealed record AssistEventDto(DateTime OccurredAtUtc);

/// <summary>
/// Represents an own goal event for a player.
/// </summary>
/// <param name="OccurredAtUtc">The UTC timestamp when the own goal was scored.</param>
public sealed record OwnGoalEventDto(DateTime OccurredAtUtc);

/// <summary>
/// Represents the substitution status of a player.
/// </summary>
/// <param name="IsSubstitutedOn">Whether the player was substituted on.</param>
/// <param name="IsSubstitutedOff">Whether the player was substituted off.</param>
/// <param name="OccurredAtUtc">The UTC timestamp when the substitution occurred.</param>
public sealed record SubstitutionStatusDto(
    bool IsSubstitutedOn,
    bool IsSubstitutedOff,
    DateTime OccurredAtUtc);
