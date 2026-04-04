using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetByDate;

internal sealed class GetFixturesByDateQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetFixturesByDateQuery, List<FixtureResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<FixtureResponse>>> Handle(GetFixturesByDateQuery request, CancellationToken cancellationToken)
    {

        return await _dbContext.Fixtures
            .AsNoTracking()
            .Where(fixture => fixture.KickoffTimeUtc.Value >= request.FixtureDate.ToDateTime(TimeOnly.MinValue)
                              && fixture.KickoffTimeUtc.Value <= request.FixtureDate.ToDateTime(TimeOnly.MaxValue))
            .Join(_dbContext.Competitions.AsNoTracking(),
                  fixture => fixture.CompetitionId,
                  competition => competition.Id,
                  (fixture, competition) => fixture.ToResponse(competition))
            .ToListAsync(cancellationToken);
    }
}
