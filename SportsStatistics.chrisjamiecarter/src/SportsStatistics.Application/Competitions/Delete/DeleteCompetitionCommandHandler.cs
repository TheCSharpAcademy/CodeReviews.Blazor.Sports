using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Delete;

internal sealed class DeleteCompetitionCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteCompetitionCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(DeleteCompetitionCommand request, CancellationToken cancellationToken)
    {
        var competition = await _dbContext.Competitions
            .Where(competition => competition.Id == request.CompetitionId)
            .SingleOrDefaultAsync(cancellationToken);

        if (competition is null)
        {
            return Result.Failure(CompetitionErrors.NotFound(request.CompetitionId));
        }

        var hasNonScheduledFixtures = await _dbContext.Fixtures
            .AsNoTracking()
            .Where(fixture => fixture.CompetitionId == request.CompetitionId
                && fixture.Status != Status.Scheduled)
            .AnyAsync(cancellationToken);

        if (hasNonScheduledFixtures)
        {
            return Result.Failure(CompetitionErrors.ContainsNonScheduledFixtures);
        }

        competition.Delete(DateTime.UtcNow);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
