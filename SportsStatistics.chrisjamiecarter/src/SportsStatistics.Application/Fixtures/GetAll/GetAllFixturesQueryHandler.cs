using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetAll;

internal sealed class GetAllFixturesQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetAllFixturesQuery, List<FixtureResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<FixtureResponse>>> Handle(GetAllFixturesQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Fixtures
            .AsNoTracking()
            .Join(_dbContext.Competitions.AsNoTracking(),
                fixture => fixture.CompetitionId,
                competition => competition.Id,
                (fixture, competition) => fixture.ToResponse(competition))
            .ToListAsync(cancellationToken);
    }
}
