using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Teamsheets;

public sealed class Teamsheet : Entity, ISoftDeletableEntity
{
    public const int RequiredNumberOfStarters = 11;

    private readonly List<TeamsheetPlayer> _players = [];

    private Teamsheet(Guid fixtureId,
                      DateTime submittedAtUtc)
        : base(Guid.CreateVersion7())
    {
        FixtureId = fixtureId;
        SubmittedAtUtc = submittedAtUtc;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Teamsheet"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    private Teamsheet() { }

    public Guid FixtureId { get; private set; }

    public DateTime SubmittedAtUtc { get; private set; }

    public IReadOnlyCollection<TeamsheetPlayer> Players => _players.AsReadOnly();

    public DateTime? DeletedOnUtc { get; private set; }

    public bool Deleted { get; private set; }

    public static Teamsheet Create(Guid fixtureId, DateTime submittedAtUtc)
    {
        var teamsheet = new Teamsheet(fixtureId, submittedAtUtc);

        teamsheet.Raise(new TeamsheetCreatedDomainEvent(teamsheet.Id, fixtureId));

        return teamsheet;
    }

    public void AddStarter(Guid playerId)
    {
        var teamsheetPlayer = TeamsheetPlayer.CreateStarter(Id, playerId);
        _players.Add(teamsheetPlayer);
    }

    public void AddSubstitute(Guid playerId)
    {
        var teamsheetPlayer = TeamsheetPlayer.CreateSubstitute(Id, playerId);
        _players.Add(teamsheetPlayer);
    }

    public IEnumerable<Guid> GetStarterIds() =>
        _players.Where(p => p.IsStarter).Select(p => p.PlayerId);

    public IEnumerable<Guid> GetSubstituteIds() =>
        _players.Where(p => !p.IsStarter).Select(p => p.PlayerId);

    public void Delete(DateTime utcNow)
    {
        if (Deleted)
        {
            return;
        }

        Deleted = true;
        DeletedOnUtc = utcNow;
        Raise(new TeamsheetDeletedDomainEvent(Id));
    }
}
