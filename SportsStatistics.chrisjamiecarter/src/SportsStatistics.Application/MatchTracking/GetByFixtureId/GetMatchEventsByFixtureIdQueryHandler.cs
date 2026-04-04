using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.GetByFixtureId;

internal sealed class GetMatchEventsByFixtureIdQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetMatchEventsByFixtureIdQuery, List<MatchEventResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<MatchEventResponse>>> Handle(GetMatchEventsByFixtureIdQuery request, CancellationToken cancellationToken)
    {
        var matchEvents = await _dbContext.MatchEvents
            .AsNoTracking()
            .Where(e => e.FixtureId == request.FixtureId)
            .Select(e => new
            {
                e.Id,
                e.FixtureId,
                EventTypeName = e.Type.Name,
                EventTypeDisplay = e.Type.GetDescription(),
                MinuteDisplay = e.Minute.Display,
                e.OccurredAtUtc
            })
            .ToListAsync(cancellationToken);

        var matchEventResponses = matchEvents
            .Select(e => new MatchEventResponse(
                e.Id,
                e.FixtureId,
                e.EventTypeName,
                e.EventTypeDisplay,
                e.MinuteDisplay,
                e.OccurredAtUtc,
                null))
            .ToList();

        var playerEventData = await _dbContext.PlayerEvents
            .AsNoTracking()
            .Where(e => e.FixtureId == request.FixtureId)
            .Join(
                _dbContext.Players.AsNoTracking(),
                pe => pe.PlayerId,
                p => p.Id,
                (pe, p) => new
                {
                    pe.Id,
                    pe.FixtureId,
                    EventTypeName = pe.Type.Name,
                    EventTypeDisplay = $"{pe.Type.GetDescription()} ({p.Name.Value})",
                    MinuteDisplay = pe.Minute.Display,
                    pe.OccurredAtUtc,
                    pe.PlayerId
                })
            .ToListAsync(cancellationToken);

        var playerEventResponses = playerEventData
            .Select(e => new MatchEventResponse(
                e.Id,
                e.FixtureId,
                e.EventTypeName,
                e.EventTypeDisplay,
                e.MinuteDisplay,
                e.OccurredAtUtc,
                e.PlayerId))
            .ToList();

        var substitutionEventData = await _dbContext.SubstitutionEvents
            .AsNoTracking()
            .Where(e => e.FixtureId == request.FixtureId)
            .Join(_dbContext.Players.AsNoTracking(),
                e => e.Substitution.PlayerOffId,
                p => p.Id,
                (e, playerOff) => new
                {
                    e.Id,
                    e.FixtureId,
                    DisplayMinute = e.Minute.Display,
                    e.OccurredAtUtc,
                    PlayerOffName = playerOff.Name.Value,
                    e.Substitution.PlayerOffId,
                    e.Substitution.PlayerOnId
                })
            .Join(_dbContext.Players.AsNoTracking(),
                x => x.PlayerOnId,
                p => p.Id,
                (x, playerOn) => new
                {
                    x.Id,
                    x.FixtureId,
                    x.DisplayMinute,
                    x.OccurredAtUtc,
                    x.PlayerOffName,
                    PlayerOnName = playerOn.Name.Value,
                    x.PlayerOffId
                })
            .ToListAsync(cancellationToken);

        var substitutionEventResponses = substitutionEventData
            .Select(e => new MatchEventResponse(
                e.Id,
                e.FixtureId,
                "Substitution",
                $"Substitution ({e.PlayerOffName} → {e.PlayerOnName})",
                e.DisplayMinute,
                e.OccurredAtUtc,
                e.PlayerOffId))
            .ToList();

        var allEvents = matchEventResponses
            .Concat(playerEventResponses)
            .Concat(substitutionEventResponses)
            .OrderBy(e => e.OccurredAtUtc)
            .ToList();

        return allEvents;
    }
}
