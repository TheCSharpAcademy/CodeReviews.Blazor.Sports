using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Reports.GetPlayerFixtureStatistics;

internal sealed class GetPlayerFixtureStatisticsQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetPlayerFixtureStatisticsQuery, List<PlayerFixtureStatisticsResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<PlayerFixtureStatisticsResponse>>> Handle(GetPlayerFixtureStatisticsQuery request, CancellationToken cancellationToken)
    {
        var response = new List<PlayerFixtureStatisticsResponse>();

        var teamsheet = await _dbContext.Teamsheets
            .AsNoTracking()
            .Include(teamsheet => teamsheet.Players)
            .Where(teamsheet => teamsheet.FixtureId == request.FixtureId)
            .SingleOrDefaultAsync(cancellationToken);

        if (teamsheet is null)
        {
            return response;
        }

        var playerIds = teamsheet.Players.Select(player => player.PlayerId).ToList();
        var playerDictionary = await _dbContext.Players
            .AsNoTracking()
            .Where(player => playerIds.Contains(player.Id))
            .ToDictionaryAsync(player => player.Id, cancellationToken);

        var playerEventDictionary = await _dbContext.PlayerEvents
            .AsNoTracking()
            .Where(playerEvent => playerEvent.FixtureId == request.FixtureId)
            .GroupBy(playerEvent => playerEvent.PlayerId)
            .ToDictionaryAsync(group => group.Key, cancellationToken);

        var substitutionEvents = await _dbContext.SubstitutionEvents
            .AsNoTracking()
            .Where(e => e.FixtureId == request.FixtureId)
            .ToListAsync(cancellationToken);

        var firstHalfFinishedAtUtc = await _dbContext.MatchEvents
            .AsNoTracking()
            .Where(e => e.FixtureId == request.FixtureId && e.Type == MatchEventType.FirstHalfFinished)
            .Select(e => e.OccurredAtUtc)
            .FirstOrDefaultAsync(cancellationToken);

        var secondHalfStartedAtUtc = await _dbContext.MatchEvents
            .AsNoTracking()
            .Where(e => e.FixtureId == request.FixtureId && e.Type == MatchEventType.SecondHalfStarted)
            .Select(e => e.OccurredAtUtc)
            .FirstOrDefaultAsync(cancellationToken);

        var statistics = new List<PlayerFixtureStatistics>();
        foreach (var teamsheetPlayer in teamsheet.Players)
        {
            if (!playerDictionary.TryGetValue(teamsheetPlayer.PlayerId, out var player))
            {
                continue;
            }

            playerEventDictionary.TryGetValue(teamsheetPlayer.PlayerId, out var playerEvents);

            var minutesPlayed = CalculateMinutesPlayed(
                player.Id,
                teamsheetPlayer.IsStarter,
                substitutionEvents,
                playerEvents?.FirstOrDefault(e => e.Type == PlayerEventType.RedCard),
                firstHalfFinishedAtUtc,
                secondHalfStartedAtUtc);

            statistics.Add(new PlayerFixtureStatistics(
                player.Id,
                player.Name,
                player.Position,
                minutesPlayed,
                playerEvents?.Count(e => e.Type == PlayerEventType.PassSuccess) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.PassFailure) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.ShotOnTarget) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.ShotOffTarget) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.Goal) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.GoalAssist) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.OwnGoal) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.Save) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.Tackle) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.FoulWon) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.FoulConceded) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.YellowCard) ?? 0,
                playerEvents?.Count(e => e.Type == PlayerEventType.RedCard) ?? 0));
        }

        return statistics.Select(statistic => new PlayerFixtureStatisticsResponse(
            statistic.PlayerId,
            statistic.PlayerName,
            statistic.Position,
            statistic.MinutesPlayed,
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
            statistic.RedCardCount)).ToList();
    }

    private int CalculateMinutesPlayed(
        Guid playerId,
        bool isStarter,
        List<SubstitutionEvent> substitutionEvents,
        PlayerEvent? redCardEvent,
        DateTime firstHalfFinishedAtUtc,
        DateTime secondHalfStartedAtUtc)
    {
        var subbedOnEvent = substitutionEvents
            .Where(s => s.Substitution.PlayerOnId == playerId)
            .FirstOrDefault();

        var subbedOffEvent = substitutionEvents
            .Where(s => s.Substitution.PlayerOffId == playerId)
            .FirstOrDefault();

        // Non-starter who never came on.
        if (!isStarter && subbedOnEvent is null)
        {
            return 0;
        }

        // Start: minute 0 for starters, or the minute they were subbed on.
        int startMinute = isStarter 
            ? 0 
            : NormaliseMinute(subbedOnEvent!.Minute.BaseMinute, subbedOnEvent.OccurredAtUtc, firstHalfFinishedAtUtc, secondHalfStartedAtUtc);

        // End: default to second half end minute.
        int endMinute = Minute.SecondHalfEndMinute;

        if (subbedOffEvent is not null)
        {
            int subbedOffMinute = NormaliseMinute(subbedOffEvent.Minute.BaseMinute, subbedOffEvent.OccurredAtUtc, firstHalfFinishedAtUtc, secondHalfStartedAtUtc);
            endMinute = Math.Min(endMinute, subbedOffMinute);
        }

        if (redCardEvent is not null)
        {
            int redCardMinute = NormaliseMinute(redCardEvent.Minute.BaseMinute, redCardEvent.OccurredAtUtc, firstHalfFinishedAtUtc, secondHalfStartedAtUtc);
            endMinute = Math.Min(endMinute, redCardMinute);
        }

        return Math.Max(0, endMinute - startMinute);
    }

    private static int NormaliseMinute(
        int baseMinute,
        DateTime occurredAtUtc,
        DateTime firstHalfFinishedAtUtc,
        DateTime secondHalfStartedAtUtc)
    {
        bool isHalfTimeEvent = occurredAtUtc >= firstHalfFinishedAtUtc
                            && occurredAtUtc <= secondHalfStartedAtUtc;

        return isHalfTimeEvent ? Minute.FirstHalfEndMinute : baseMinute;
    }
}
