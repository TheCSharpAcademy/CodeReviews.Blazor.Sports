using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.SubstitutionEvents.Create;

internal sealed class CreateSubstitutionEventCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateSubstitutionEventCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreateSubstitutionEventCommand request, CancellationToken cancellationToken)
    {
        var fixtureExists = await _dbContext.Fixtures
            .AsNoTracking()
            .AnyAsync(f => f.Id == request.FixtureId, cancellationToken);

        if (!fixtureExists)
        {
            return Result.Failure(FixtureErrors.NotFound(request.FixtureId));
        }

        var substitutionResult = Substitution.Create(request.PlayerOffId, request.PlayerOnId);
        if (substitutionResult.IsFailure)
        {
            return Result.Failure(substitutionResult.Error);
        }

        var minuteResult = Minute.Create(request.BaseMinute, request.StoppageMinute);
        if (minuteResult.IsFailure)
        {
            return Result.Failure(minuteResult.Error);
        }

        var substitutionEvent = SubstitutionEvent.Create(
            request.FixtureId,
            substitutionResult.Value,
            minuteResult.Value,
            request.OccurredAtUtc);

        _dbContext.SubstitutionEvents.Add(substitutionEvent);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
