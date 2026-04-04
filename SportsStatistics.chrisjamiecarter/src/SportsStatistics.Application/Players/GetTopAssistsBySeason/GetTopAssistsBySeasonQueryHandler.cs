using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.GetTopAssistsBySeason;

internal sealed class GetTopAssistsBySeasonQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetTopAssistsBySeasonQuery, List<TopAssistsResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<TopAssistsResponse>>> Handle(GetTopAssistsBySeasonQuery request, CancellationToken cancellationToken)
    {
        var fixtureIds = _dbContext.Fixtures
            .Join(
                _dbContext.Competitions.Where(competition => competition.SeasonId == request.SeasonId),
                fixture => fixture.CompetitionId,
                competition => competition.Id,
                (fixture, _) => fixture.Id);

        return await _dbContext.PlayerEvents
            .AsNoTracking()
            .Where(playerEvent => fixtureIds.Contains(playerEvent.FixtureId) && playerEvent.Type == PlayerEventType.GoalAssist)
            .GroupBy(playerEvent => playerEvent.PlayerId)
            .Select(group => new { PlayerId = group.Key, AssistCount = group.Count() })
            .OrderByDescending(x => x.AssistCount)
            .Take(request.Count)
            .Join(
                _dbContext.Players,
                assister => assister.PlayerId,
                player => player.Id,
                (assister, player) => new TopAssistsResponse(
                    player.Id,
                    player.Name,
                    player.Position,
                    assister.AssistCount))
            .ToListAsync(cancellationToken);
    }
}
