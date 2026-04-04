using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Teamsheets;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Teamsheets.GetByFixtureId;

internal sealed class GetTeamsheetByFixtureIdQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetTeamsheetByFixtureIdQuery, TeamsheetResponse>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<TeamsheetResponse>> Handle(GetTeamsheetByFixtureIdQuery request, CancellationToken cancellationToken)
    {
        var teamsheet = await _dbContext.Teamsheets
            .AsNoTracking()
            .Where(teamsheet => teamsheet.FixtureId == request.FixtureId)
            .FirstOrDefaultAsync(cancellationToken);

        if (teamsheet is null)
        {
            return Result.Failure<TeamsheetResponse>(TeamsheetErrors.NotFound(request.FixtureId));
        }

        var teamsheetPlayers = await _dbContext.TeamsheetPlayers
            .AsNoTracking()
            .Where(tp => tp.TeamsheetId == teamsheet.Id)
            .Join(
                _dbContext.Players.AsNoTracking(),
                tp => tp.PlayerId,
                p => p.Id,
                (tp, p) => new
                {
                    tp.IsStarter,
                    tp.PlayerId,
                    Name = p.Name.Value,
                    SquadNumber = p.SquadNumber.Value,
                    Position = p.Position.Name
                })
            .ToListAsync(cancellationToken);

        var starters = teamsheetPlayers
            .Where(p => p.IsStarter)
            .Select(p => new TeamsheetPlayerResponse(
                p.PlayerId,
                p.Name,
                p.SquadNumber,
                p.Position))
            .ToList();

        var substitutes = teamsheetPlayers
            .Where(p => !p.IsStarter)
            .Select(p => new TeamsheetPlayerResponse(
                p.PlayerId,
                p.Name,
                p.SquadNumber,
                p.Position))
            .ToList();

        return teamsheet.ToResponse(starters, substitutes);
    }
}
