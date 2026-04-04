using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed class Player : Entity
{
    private Player(
        Name name,
        SquadNumber squadNumber,
        Nationality nationality,
        DateOfBirth dateOfBirth,
        Position position)
        : base(Guid.CreateVersion7())
    {
        Name = name;
        SquadNumber = squadNumber;
        Nationality = nationality;
        DateOfBirth = dateOfBirth;
        Position = position;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    private Player() { }

    public Name Name { get; private set; } = default!;

    public SquadNumber SquadNumber { get; private set; } = default!;

    public Nationality Nationality { get; private set; } = default!;

    public DateOfBirth DateOfBirth { get; private set; } = default!;

    public Position Position { get; private set; } = default!;

    public bool LeftClub { get; private set; }

    public DateTime? LeftClubOnUtc { get; private set; }

    public int Age => DateOfBirth.Value.CalculateAge();

    public static Player Create(Name name, SquadNumber squadNumber, Nationality nationality, DateOfBirth dateOfBirth, Position position)
    {
        var player = new Player(name, squadNumber, nationality, dateOfBirth, position);

        player.Raise(new PlayerCreatedDomainEvent(player.Id));

        return player;
    }

    public bool ChangeName(Name name)
    {
        if (Name == name)
        {
            return false;
        }

        var previousName = Name;
        Name = name;
        Raise(new PlayerNameChangedDomainEvent(this, previousName));

        return true;
    }

    public bool ChangeSquadNumber(SquadNumber squadNumber)
    {
        if (SquadNumber == squadNumber)
        {
            return false;
        }

        var previousSquadNumber = SquadNumber;
        SquadNumber = squadNumber;
        Raise(new PlayerSquadNumberChangedDomainEvent(this, previousSquadNumber));

        return true;
    }

    public bool ChangeNationality(Nationality nationality)
    {
        if (Nationality == nationality)
        {
            return false;
        }

        var previousNationality = Nationality;
        Nationality = nationality;
        Raise(new PlayerNationalityChangedDomainEvent(this, previousNationality));

        return true;
    }

    public bool ChangeDateOfBirth(DateOfBirth dateOfBirth)
    {
        if (DateOfBirth == dateOfBirth)
        {
            return false;
        }

        var previousDateOfBirth = DateOfBirth;
        DateOfBirth = dateOfBirth;
        Raise(new PlayerDateOfBirthChangedDomainEvent(this, previousDateOfBirth));

        return true;
    }

    public bool ChangePosition(Position position)
    {
        if (Position == position)
        {
            return false;
        }

        var previousPosition = Position;
        Position = position;
        Raise(new PlayerPositionChangedDomainEvent(this, previousPosition));

        return true;
    }

    public bool ChangeLeftClub(bool leftClub, TimeProvider? timeProvider = null)
    {
        if (LeftClub == leftClub)
        {
            return false;
        }

        if (leftClub)
        {
            LeaveClub(timeProvider ?? TimeProvider.System);
        }
        else
        {
            RejoinClub();
        }

        return true;
    }

    private void LeaveClub(TimeProvider timeProvider)
    {
        if (LeftClub)
        {
            return;
        }

        var utcNow = timeProvider.GetUtcNow().UtcDateTime;

        LeftClub = true;
        LeftClubOnUtc = utcNow;
        Raise(new PlayerLeftClubDomainEvent(this, utcNow));
    }

    private void RejoinClub()
    {
        if (!LeftClub)
        {
            return;
        }

        var previousLeftClubOnUtc = LeftClubOnUtc;
        LeftClub = false;
        LeftClubOnUtc = null;
        Raise(new PlayerRejoinedClubDomainEvent(this, previousLeftClubOnUtc));
    }
}
