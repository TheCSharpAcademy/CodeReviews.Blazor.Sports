using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Seasons;

public sealed class Season : Entity, ISoftDeletableEntity
{
    private Season(DateRange dateRange)
        : base(Guid.CreateVersion7())
    {
        DateRange = dateRange;
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="Season"/> class.
    /// </summary>
    /// <remarks>
    /// Required for Entity Framework Core.
    /// </remarks>
    private Season() { }

    public DateRange DateRange { get; private set; } = default!;

    public string Name => $"{DateRange.StartDate.Year}/{DateRange.EndDate.Year}";

    public DateTime? DeletedOnUtc { get; private set; }

    public bool Deleted { get; private set; }

    public static Season Create(DateRange dateRange)
    {
        var season = new Season(dateRange);

        season.Raise(new SeasonCreatedDomainEvent(season.Id));

        return season;
    }

    public bool ChangeDateRange(DateRange dateRange)
    {
        if (DateRange == dateRange)
        {
            return false;
        }

        var previousDateRange = DateRange;
        DateRange = dateRange;
        Raise(new SeasonDateRangeChangedDomainEvent(this, previousDateRange));

        return true;
    }

    public Competition CreateCompetition(Name name, Format format)
    {
        return Competition.Create(this, name, format);
    }

    public void Delete(DateTime utcNow)
    {
        if (Deleted)
        {
            return;
        }

        Deleted = true;
        DeletedOnUtc = utcNow;
        Raise(new SeasonDeletedDomainEvent(Id));
    }
}
