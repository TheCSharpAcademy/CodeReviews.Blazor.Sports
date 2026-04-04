using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Reports.GetPlayerSeasonStatistics;

internal sealed class GetPlayerSeasonStatisticsQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetPlayerSeasonStatisticsQuery, List<PlayerSeasonStatisticsResponse>>
{
    private sealed class PlayerAppearances(int starts, int substitutions)
    {
        public int Starts { get; set; } = starts;
        public int Substitutions { get; set; } = substitutions;

        public int Total => Starts + Substitutions;
    }

    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<PlayerSeasonStatisticsResponse>>> Handle(GetPlayerSeasonStatisticsQuery request, CancellationToken cancellationToken)
    {
        var completedFixtureIds = await _dbContext.Fixtures
            .AsNoTracking()
            .Where(fixture => fixture.Status == Status.Completed)
            .Join(_dbContext.Competitions
                .AsNoTracking()
                .Where(competition => competition.SeasonId == request.SeasonId),
                    fixture => fixture.CompetitionId,
                    competition => competition.Id,
                    (fixture, _) => fixture)
            .Select(fixture => fixture.Id)
            .ToListAsync(cancellationToken);

        if (completedFixtureIds.Count == 0)
        {
            return new List<PlayerSeasonStatisticsResponse>();
        }

        var teamsheets = await _dbContext.Teamsheets
            .AsNoTracking()
            .Include(teamsheet => teamsheet.Players)
            .Where(teamsheet => completedFixtureIds.Contains(teamsheet.FixtureId))
            .ToDictionaryAsync(teamsheet => teamsheet.FixtureId, cancellationToken);

        var playerEvents = await _dbContext.PlayerEvents
            .AsNoTracking()
            .Where(playerEvent => completedFixtureIds.Contains(playerEvent.FixtureId))
            .ToListAsync(cancellationToken);

        var substitutionEvents = await _dbContext.SubstitutionEvents
            .AsNoTracking()
            .Where(substitutionEvent => completedFixtureIds.Contains(substitutionEvent.FixtureId))
            .ToListAsync(cancellationToken);

        var players = await _dbContext.Players
            .AsNoTracking()
            .ToDictionaryAsync(player => player.Id, cancellationToken);

        var playerAppearances = new Dictionary<Guid, PlayerAppearances>();
        foreach (var fixtureId in completedFixtureIds)
        {
            var teamsheet = teamsheets[fixtureId];
            foreach (var playerId in teamsheet?.GetStarterIds() ?? [])
            {
                if (playerAppearances.TryGetValue(playerId, out var playerAppearance))
                {
                    playerAppearance.Starts++;
                }
                else
                {
                    playerAppearances.TryAdd(playerId, new PlayerAppearances(1, 0));
                }
            }
        }

        foreach (var substitutionEvent in substitutionEvents)
        {
            var playerId = substitutionEvent.Substitution.PlayerOnId;
            if (playerAppearances.TryGetValue(playerId, out var playerAppearance))
            {
                playerAppearance.Substitutions++;
            }
            else
            {
                playerAppearances.TryAdd(playerId, new PlayerAppearances(0, 1));
            }
        }

        var statistics = playerEvents
            .GroupBy(playerEvent => playerEvent.PlayerId)
            .Select(group =>
            {
                var player = players[group.Key];
                var appearances = playerAppearances[group.Key];
                return new PlayerSeasonStatistics(
                    player.Id,
                    player.Name,
                    player.Position,
                    appearances?.Total ?? 0,
                    appearances?.Starts ?? 0,
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.PassSuccess),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.PassFailure),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.ShotOnTarget),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.ShotOffTarget),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.Goal),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.GoalAssist),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.OwnGoal),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.Save),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.Tackle),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.FoulWon),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.FoulConceded),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.YellowCard),
                    group.Count(playerEvent => playerEvent.Type == PlayerEventType.RedCard));
            });

        var maxFixturesPlayed = statistics.Max(statistic => statistic.FixturesPlayed);
        var maxFixturesStarted = statistics.Max(statistic => statistic.FixturesStarted);
        var maxPassCount = statistics.Max(statistic => statistic.PassCount);
        var maxPassSuccessCount = statistics.Max(statistic => statistic.PassSuccessCount);
        var maxPassFailureCount = statistics.Max(statistic => statistic.PassFailureCount);
        var maxShotCount = statistics.Max(statistic => statistic.ShotCount);
        var maxShotOnTargetCount = statistics.Max(statistic => statistic.ShotOnTargetCount);
        var maxShotOffTargetCount = statistics.Max(statistic => statistic.ShotOffTargetCount);
        var maxGoalCount = statistics.Max(statistic => statistic.GoalCount);
        var maxGoalAssistCount = statistics.Max(statistic => statistic.GoalAssistCount);
        var maxOwnGoalCount = statistics.Max(statistic => statistic.OwnGoalCount);
        var maxSaveCount = statistics.Max(statistic => statistic.SaveCount);
        var maxTackleCount = statistics.Max(statistic => statistic.TackleCount);
        var maxFoulWonCount = statistics.Max(statistic => statistic.FoulWonCount);
        var maxFoulConcededCount = statistics.Max(statistic => statistic.FoulConcededCount);
        var maxYellowCardCount = statistics.Max(statistic => statistic.YellowCardCount);
        var maxRedCardCount = statistics.Max(statistic => statistic.RedCardCount);

        return statistics.Select(statistic => new PlayerSeasonStatisticsResponse(
            statistic.PlayerId,
            statistic.PlayerName,
            statistic.Position,
            statistic.FixturesStarted,
            statistic.FixturesPlayed,
            statistic.PassCount,
            statistic.PassSuccessCount,
            statistic.PassFailureCount,
            statistic.ShotCount,
            statistic.ShotOnTargetCount,
            statistic.ShotOffTargetCount,
            statistic.GoalCount,
            statistic.GoalAssistCount,
            statistic.OwnGoalCount,
            statistic.SaveCount,
            statistic.TackleCount,
            statistic.FoulWonCount,
            statistic.FoulConcededCount,
            statistic.YellowCardCount,
            statistic.RedCardCount,
            maxFixturesPlayed,
            maxFixturesStarted,
            maxPassCount,
            maxPassSuccessCount,
            maxPassFailureCount,
            maxShotCount,
            maxShotOnTargetCount,
            maxShotOffTargetCount,
            maxGoalCount,
            maxGoalAssistCount,
            maxOwnGoalCount,
            maxSaveCount,
            maxTackleCount,
            maxFoulWonCount,
            maxFoulConcededCount,
            maxYellowCardCount,
            maxRedCardCount)).ToList();
    }    
}
