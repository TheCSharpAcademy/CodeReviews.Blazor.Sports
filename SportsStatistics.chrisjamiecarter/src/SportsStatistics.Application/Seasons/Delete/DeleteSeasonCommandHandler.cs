using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons.Delete;

internal sealed class DeleteSeasonCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteSeasonCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(DeleteSeasonCommand request, CancellationToken cancellationToken)
    {
        var season = await _dbContext.Seasons
            .Where(season => season.Id == request.SeasonId)
            .SingleOrDefaultAsync(cancellationToken);

        if (season is null)
        {
            return Result.Failure(SeasonErrors.NotFound(request.SeasonId));
        }

        var hasNonScheduledFixtures = await _dbContext.Fixtures
            .AsNoTracking()
            .Join(
                _dbContext.Competitions.AsNoTracking().Where(competition => competition.SeasonId == season.Id),
                fixture => fixture.CompetitionId,
                competition => competition.Id,
                (fixture, _) => fixture)
            .Where(fixture => fixture.Status != Status.Scheduled)
            .AnyAsync(cancellationToken);

        if (hasNonScheduledFixtures)
        {
            return Result.Failure(SeasonErrors.ContainsNonScheduledFixtures);
        }

        season.Delete(DateTime.UtcNow);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
