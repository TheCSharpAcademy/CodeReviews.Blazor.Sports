using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetCompletedBySeasonId;

internal sealed class GetCompletedFixturesBySeasonIdQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetCompletedFixturesBySeasonIdQuery, List<FixtureResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<FixtureResponse>>> Handle(GetCompletedFixturesBySeasonIdQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Fixtures
            .AsNoTracking()
            .Where(fixture => fixture.Status == Status.Completed)
            .Join(
                _dbContext.Competitions.AsNoTracking().Where(competition => competition.SeasonId == request.SeasonId),
                fixture => fixture.CompetitionId,
                competition => competition.Id,
                (fixture, competition) => fixture.ToResponse(competition))
            .ToListAsync(cancellationToken);
    }
}
