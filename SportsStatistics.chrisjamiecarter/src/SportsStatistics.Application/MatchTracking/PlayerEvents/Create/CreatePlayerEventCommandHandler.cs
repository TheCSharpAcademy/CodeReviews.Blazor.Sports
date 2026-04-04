using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.PlayerEvents.Create;

internal sealed class CreatePlayerEventCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreatePlayerEventCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreatePlayerEventCommand request, CancellationToken cancellationToken)
    {
        var fixtureExists = await _dbContext.Fixtures
            .AsNoTracking()
            .AnyAsync(f => f.Id == request.FixtureId, cancellationToken);

        if (!fixtureExists)
        {
            return Result.Failure(FixtureErrors.NotFound(request.FixtureId));
        }

        var playerExists = await _dbContext.Players
            .AsNoTracking()
            .AnyAsync(p => p.Id == request.PlayerId, cancellationToken);

        if (!playerExists)
        {
            return Result.Failure(PlayerErrors.NotFound(request.PlayerId));
        }

        var playerEventTypeResult = PlayerEventType.Resolve(request.PlayerEventTypeId);
        if (playerEventTypeResult.IsFailure)
        {
            return Result.Failure(MatchEventBaseErrors.PlayerEventType.Unknown);
        }

        var minuteResult = Minute.Create(request.BaseMinute, request.StoppageMinute);
        if (minuteResult.IsFailure)
        {
            return Result.Failure(minuteResult.Error);
        }

        var playerEvent = PlayerEvent.Create(
            request.FixtureId,
            request.PlayerId,
            playerEventTypeResult.Value,
            minuteResult.Value,
            request.OccurredAtUtc);

        _dbContext.PlayerEvents.Add(playerEvent);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
