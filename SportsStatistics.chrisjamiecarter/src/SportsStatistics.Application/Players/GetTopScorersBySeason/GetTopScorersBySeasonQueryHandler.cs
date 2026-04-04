using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.GetTopScorersBySeason;

internal sealed class GetTopScorersBySeasonQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetTopScorersBySeasonQuery, List<TopScorersResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<TopScorersResponse>>> Handle(GetTopScorersBySeasonQuery request, CancellationToken cancellationToken)
    {
        var fixtureIds = _dbContext.Fixtures
            .Join(
                _dbContext.Competitions.Where(competition => competition.SeasonId == request.SeasonId),
                fixture => fixture.CompetitionId,
                competition => competition.Id,
                (fixture, _) => fixture.Id);

        return await _dbContext.PlayerEvents
            .AsNoTracking()
            .Where(playerEvent => fixtureIds.Contains(playerEvent.FixtureId) && playerEvent.Type == PlayerEventType.Goal)
            .GroupBy(playerEvent => playerEvent.PlayerId)
            .Select(group => new { PlayerId = group.Key, GoalCount = group.Count() })
            .OrderByDescending(x => x.GoalCount)
            .Take(request.Count)
            .Join(
                _dbContext.Players,
                scorer => scorer.PlayerId,
                player => player.Id,
                (scorer, player) => new TopScorersResponse(
                    player.Id,
                    player.Name,
                    player.Position,
                    scorer.GoalCount))
            .ToListAsync(cancellationToken);
    }
}
