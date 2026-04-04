using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Competitions;

public sealed class Competition : Entity, ISoftDeletableEntity
{
    private Competition(Guid seasonId,
                        Name name,
                        Format format)
        : base(Guid.CreateVersion7())
    {
        SeasonId = seasonId;
        Name = name;
        Format = format;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Competition"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    private Competition() { }

    public Guid SeasonId { get; private set; } = default!;

    public Name Name { get; private set; } = default!;

    public Format Format { get; private set; } = default!;

    public DateTime? DeletedOnUtc { get; private set; }

    public bool Deleted { get; private set; }

    internal static Competition Create(Season season, Name name, Format format)
    {
        var competition = new Competition(season.Id, name, format);

        competition.Raise(new CompetitionCreatedDomainEvent(competition.Id));

        return competition;
    }

    public bool ChangeName(Name name)
    {
        if (Name == name)
        {
            return false;
        }

        var previousName = Name;
        Name = name;
        Raise(new CompetitionNameChangedDomainEvent(this, previousName));

        return true;
    }

    public bool ChangeFormat(Format format)
    {
        if (Format == format)
        {
            return false;
        }

        var previousFormat = Format;
        Format = format;
        Raise(new CompetitionFormatChangedDomainEvent(this, previousFormat));

        return true;
    }

    public Fixture CreateFixture(Opponent opponent, KickoffTimeUtc kickoffTimeUtc, Location location)
    {
        return Fixture.Create(this, opponent, kickoffTimeUtc, location);
    }

    public void Delete(DateTime utcNow)
    {
        if (Deleted)
        {
            return;
        }

        Deleted = true;
        DeletedOnUtc = utcNow;
        Raise(new CompetitionDeletedDomainEvent(Id));
    }
}
