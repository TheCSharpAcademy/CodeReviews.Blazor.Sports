using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.Create;

internal sealed class CreateFixtureCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateFixtureCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreateFixtureCommand request, CancellationToken cancellationToken)
    {
        var competition = await _dbContext.Competitions.AsNoTracking()
                                                       .Where(competition => competition.Id == request.CompetitionId)
                                                       .SingleOrDefaultAsync(cancellationToken);

        if (competition is null)
        {
            return Result.Failure(CompetitionErrors.NotFound(request.CompetitionId));
        }

        var season = await _dbContext.Seasons.AsNoTracking()
                                             .Where(season => season.Id == competition.SeasonId)
                                             .SingleOrDefaultAsync(cancellationToken);

        if (season is null)
        {
            return Result.Failure(SeasonErrors.NotFound(competition.SeasonId));
        }

        var opponentResult = Opponent.Create(request.Opponent);
        var kickOffTimeUtcResult = KickoffTimeUtc.Create(request.KickoffTimeUtc);
        var locationResult = Location.Resolve(request.LocationId);
        var firstFailureOrSuccess = Result.FirstFailureOrSuccess(opponentResult,
                                                                 kickOffTimeUtcResult,
                                                                 locationResult);
        if (firstFailureOrSuccess.IsFailure)
        {
            return firstFailureOrSuccess;
        }

        var kickoffDate = DateOnly.FromDateTime(request.KickoffTimeUtc);

        if (kickoffDate < season.DateRange.StartDate || kickoffDate > season.DateRange.EndDate)
        {
            return Result.Failure(FixtureErrors.KickoffTimeOutsideSeason(request.KickoffTimeUtc, season.DateRange.StartDate, season.DateRange.EndDate));
        }

        if (await _dbContext.Fixtures.AsNoTracking()
                                     .Where(fixture => fixture.KickoffTimeUtc.Value >= kickoffDate.ToDateTime(TimeOnly.MinValue)
                                                       && fixture.KickoffTimeUtc.Value <= kickoffDate.ToDateTime(TimeOnly.MaxValue))
                                     .AnyAsync(cancellationToken))
        {
            return Result.Failure(FixtureErrors.AlreadyScheduledOnDate(kickoffDate));
        }

        var fixture = competition.CreateFixture(opponentResult.Value, kickOffTimeUtcResult.Value, locationResult.Value);

        _dbContext.Fixtures.Add(fixture);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
