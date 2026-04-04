using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Clubs;

public sealed class Club : Entity
{
    private Club(Name name) : base(Guid.CreateVersion7())
    {
        Name = name;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Club"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    private Club() { }

    public Name Name { get; private set; } = default!;

    public static Club Create(Name name)
    {
        var club = new Club(name);

        club.Raise(new ClubCreatedDomainEvent(club.Id));

        return club;
    }

    public bool ChangeName(Name name)
    {
        if (Name == name)
        {
            return false;
        }

        var previousName = Name;
        Name = name;
        Raise(new ClubNameChangedDomainEvent(this, previousName));

        return true;
    }
}
