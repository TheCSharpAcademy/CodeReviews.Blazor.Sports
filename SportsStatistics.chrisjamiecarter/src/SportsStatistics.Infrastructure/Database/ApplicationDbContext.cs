using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Domain.Clubs;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Teamsheets;
using SportsStatistics.Infrastructure.Database.Converters;
using SportsStatistics.Infrastructure.DomainEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Database;

internal sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IDomainEventsDispatcher domainEventsDispatcher)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Club> Clubs { get; set; }

    public DbSet<Competition> Competitions { get; set; }

    public DbSet<Fixture> Fixtures { get; set; }

    public DbSet<MatchEvent> MatchEvents { get; set; }

    public DbSet<Player> Players { get; set; }

    public DbSet<PlayerEvent> PlayerEvents { get; set; }

    public DbSet<Season> Seasons { get; set; }

    public DbSet<SubstitutionEvent> SubstitutionEvents { get; set; }

    public DbSet<Teamsheet> Teamsheets { get; set; }

    public DbSet<TeamsheetPlayer> TeamsheetPlayers { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Format>()
                            .HaveConversion<CompetitionFormatConverter>();

        configurationBuilder.Properties<Location>()
                            .HaveConversion<FixtureLocationConverter>();

        configurationBuilder.Properties<MatchEventType>()
                            .HaveConversion<MatchEventTypeConverter>();

        configurationBuilder.Properties<PlayerEventType>()
                            .HaveConversion<PlayerEventTypeConverter>();

        configurationBuilder.Properties<Position>()
                            .HaveConversion<PlayerPositionConverter>();

        configurationBuilder.Properties<Status>()
                            .HaveConversion<FixtureStatusConverter>();

        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                List<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        await domainEventsDispatcher.DispatchAsync(domainEvents);
    }
}
