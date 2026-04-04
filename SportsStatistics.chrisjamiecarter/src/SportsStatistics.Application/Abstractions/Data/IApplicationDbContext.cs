using Microsoft.EntityFrameworkCore;
using SportsStatistics.Domain.Clubs;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Domain.Teamsheets;

namespace SportsStatistics.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Club> Clubs { get; }

    DbSet<Competition> Competitions { get; }

    DbSet<Fixture> Fixtures { get; }

    DbSet<MatchEvent> MatchEvents { get; }

    DbSet<Player> Players { get; }

    DbSet<PlayerEvent> PlayerEvents { get; }

    DbSet<Season> Seasons { get; }

    DbSet<SubstitutionEvent> SubstitutionEvents { get; }

    DbSet<Teamsheet> Teamsheets { get; }

    DbSet<TeamsheetPlayer> TeamsheetPlayers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
