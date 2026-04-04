using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.MatchEvents.Create;

internal sealed class CreateMatchEventCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateMatchEventCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreateMatchEventCommand request, CancellationToken cancellationToken)
    {
        var fixtureExists = await _dbContext.Fixtures
            .AsNoTracking()
            .AnyAsync(f => f.Id == request.FixtureId, cancellationToken);

        if (!fixtureExists)
        {
            return Result.Failure(FixtureErrors.NotFound(request.FixtureId));
        }

        var matchEventTypeResult = MatchEventType.Resolve(request.MatchEventTypeId);
        if (matchEventTypeResult.IsFailure)
        {
            return Result.Failure(MatchEventBaseErrors.MatchEventType.Unknown);
        }

        var minuteResult = Minute.Create(request.BaseMinute, request.StoppageMinute);
        if (minuteResult.IsFailure)
        {
            return Result.Failure(minuteResult.Error);
        }

        var matchEvent = MatchEvent.Create(
            request.FixtureId,
            matchEventTypeResult.Value,
            minuteResult.Value,
            request.OccurredAtUtc);

        _dbContext.MatchEvents.Add(matchEvent);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
