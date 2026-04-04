using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetNext;

internal sealed class GetNextFixtureQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetNextFixtureQuery, FixtureResponse>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<FixtureResponse>> Handle(GetNextFixtureQuery request, CancellationToken cancellationToken)
    {
        var fixture = await _dbContext.Fixtures
            .AsNoTracking()
            .Where(fixture => fixture.Status == Status.Scheduled && fixture.KickoffTimeUtc.Value >= request.TodayEnd)
            .OrderBy(fixture => fixture.KickoffTimeUtc.Value)
            .Join(
                _dbContext.Competitions.AsNoTracking(),
                fixture => fixture.CompetitionId,
                competition => competition.Id,
                (fixture, competition) => new FixtureResponse(
                    fixture.Id,
                    competition.Name,
                    fixture.Opponent,
                    fixture.KickoffTimeUtc.Value,
                    fixture.Location,
                    fixture.Status))
            .FirstOrDefaultAsync(cancellationToken);

        return fixture is not null
            ? fixture
            : Result.Failure<FixtureResponse>(FixtureErrors.NoneFound);
    }
}
