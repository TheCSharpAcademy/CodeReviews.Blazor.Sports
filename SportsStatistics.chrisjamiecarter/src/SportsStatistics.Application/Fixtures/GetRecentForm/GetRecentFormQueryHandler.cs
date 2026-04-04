using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetRecentForm;

internal sealed class GetRecentFormQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetRecentFormQuery, List<FormReponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<FormReponse>>> Handle(GetRecentFormQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Fixtures
            .AsNoTracking()
            .Where(fixture => fixture.Status == Status.Completed)
            .Join(
                _dbContext.Competitions.AsNoTracking(),
                fixture => fixture.CompetitionId,
                competition => competition.Id,
                (fixture, competition) => fixture)
            .OrderByDescending(fixture => fixture.KickoffTimeUtc.Value)
            .Take(request.Count)
            .OrderBy(fixture => fixture.KickoffTimeUtc.Value)
            .Select(fixture => new FormReponse(
                fixture.Id,
                fixture.Outcome,
                fixture.Score.HomeGoals,
                fixture.Score.AwayGoals,
                fixture.Opponent))
            .ToListAsync(cancellationToken);
    }
}
