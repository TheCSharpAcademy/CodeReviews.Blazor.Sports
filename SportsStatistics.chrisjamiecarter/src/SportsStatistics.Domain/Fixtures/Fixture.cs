using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Fixtures;

public sealed class Fixture : Entity, ISoftDeletableEntity
{
    private Fixture(
        Guid competitionId,
        Opponent opponent,
        KickoffTimeUtc kickoffTimeUtc,
        Location location,
        Score score,
        Status status)
        : base(Guid.CreateVersion7())
    {
        CompetitionId = competitionId;
        Opponent = opponent;
        KickoffTimeUtc = kickoffTimeUtc;
        Location = location;
        Score = score;
        Status = status;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Fixture"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    private Fixture() { }

    public Guid CompetitionId { get; private set; } = default!;

    public Opponent Opponent { get; private set; } = default!;

    public KickoffTimeUtc KickoffTimeUtc { get; private set; } = default!;

    public Location Location { get; private set; } = default!;

    public Score Score { get; private set; } = default!;

    public Status Status { get; private set; } = default!;

    public DateTime? DeletedOnUtc { get; private set; }

    public bool Deleted { get; private set; }

    public int ClubGoals => Location == Location.Home || Location == Location.Neutral
        ? Score.HomeGoals
        : Score.AwayGoals;

    public int OpponentGoals => Location == Location.Home || Location == Location.Neutral
            ? Score.AwayGoals
            : Score.HomeGoals;

    public Outcome Outcome => GetOutcome();

    internal static Fixture Create(Competition competition, Opponent opponent, KickoffTimeUtc kickoffTimeUtc, Location location)
    {
        var fixture = new Fixture(
            competition.Id,
            opponent,
            kickoffTimeUtc,
            location,
            Score.Create(0, 0).Value,
            Status.Scheduled);

        fixture.Raise(new FixtureCreatedDomainEvent(fixture.Id));

        return fixture;
    }

    public Result ChangeOpponent(Opponent opponent)
    {
        if (Opponent == opponent)
        {
            return Result.Success();
        }

        if (Status != Status.Scheduled)
        {
            return Result.Failure(FixtureErrors.CannotUpdateFixtureNotScheduled);
        }

        var previousOpponent = Opponent;
        Opponent = opponent;
        Raise(new FixtureOpponentChangedDomainEvent(this, previousOpponent));

        return Result.Success();
    }

    public Result ChangeKickoffTimeUtc(KickoffTimeUtc kickoffTimeUtc)
    {
        if (KickoffTimeUtc == kickoffTimeUtc)
        {
            return Result.Success();
        }

        if (Status != Status.Scheduled)
        {
            return Result.Failure(FixtureErrors.CannotUpdateFixtureNotScheduled);
        }

        var previousKickoffTimeUtc = KickoffTimeUtc;
        KickoffTimeUtc = kickoffTimeUtc;
        Raise(new FixtureKickoffTimeUtcChangedDomainEvent(this, previousKickoffTimeUtc));

        return Result.Success();
    }

    public Result ChangeLocation(Location location)
    {
        if (Location == location)
        {
            return Result.Success();
        }

        if (Status != Status.Scheduled)
        {
            return Result.Failure(FixtureErrors.CannotUpdateFixtureNotScheduled);
        }

        var previousLocation = Location;
        Location = location;
        Raise(new FixtureLocationChangedDomainEvent(this, previousLocation));

        return Result.Success();
    }

    public Result ChangeScore(Score score)
    {
        if (Score == score)
        {
            return Result.Success();
        }

        if (Status != Status.InProgress)
        {
            return Result.Failure(FixtureErrors.CannotUpdateFixtureScoreNotInProgress);
        }

        var previousScore = Score;
        Score = score;
        Raise(new FixtureScoreChangedDomainEvent(this, previousScore));

        return Result.Success();
    }

    public Result ChangeStatus(Status status)
    {
        if (status is null)
        {
            return Result.Failure(FixtureErrors.Status.IsRequired);
        }

        if (Status == status)
        {
            return Result.Success();
        }

        if (Status == Status.Completed)
        {
            return Result.Failure(FixtureErrors.Status.CannotUpdate(Status.Name, status.Name));
        }

        if (Status == Status.InProgress && status != Status.Completed)
        {
            return Result.Failure(FixtureErrors.Status.CannotUpdate(Status.Name, status.Name));
        }

        if (Status == Status.Scheduled && status != Status.InProgress)
        {
            return Result.Failure(FixtureErrors.Status.CannotUpdate(Status.Name, status.Name));
        }

        var previousStatus = Status;
        Status = status;
        Raise(new FixtureStatusChangedDomainEvent(this, previousStatus));

        return Result.Success();
    }

    public Result Delete(DateTime utcNow)
    {
        if (Deleted)
        {
            return Result.Success();
        }

        if (Status != Status.Scheduled)
        {
            return Result.Failure(FixtureErrors.CannotDeleteNonScheduledFixture);
        }

        Deleted = true;
        DeletedOnUtc = utcNow;
        Raise(new FixtureDeletedDomainEvent(Id));

        return Result.Success();
    }

    private Outcome GetOutcome()
    {
        if (Status != Status.Completed)
        {
            return Outcome.None;
        }

        return ClubGoals > OpponentGoals
            ? Outcome.Win
            : ClubGoals < OpponentGoals
                ? Outcome.Loss
                : Outcome.Draw;
    }
}
