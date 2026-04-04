using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.GetAll;

internal sealed class GetAllPlayersQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetAllPlayersQuery, List<PlayerResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<PlayerResponse>>> Handle(GetAllPlayersQuery query, CancellationToken cancellationToken)
    {
        var queryable = _dbContext.Players.AsNoTracking();

        var filtered = query.Filter switch
        {
            PlayerClubStatusFilter.AtClub => queryable.Where(player => !player.LeftClub),
            PlayerClubStatusFilter.LeftClub => queryable.Where(player => player.LeftClub),
            _ => queryable
        };

        return await filtered
            .Select(player => new PlayerResponse(
                player.Id,
                player.Name,
                player.SquadNumber,
                player.Nationality,
                player.DateOfBirth,
                player.Position,
                player.LeftClub,
                player.LeftClubOnUtc,
                player.Age))
            .ToListAsync(cancellationToken);
    }
}
