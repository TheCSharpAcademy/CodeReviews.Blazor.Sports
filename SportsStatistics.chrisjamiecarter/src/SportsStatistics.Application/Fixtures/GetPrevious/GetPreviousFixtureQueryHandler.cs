using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetPrevious;

internal sealed class GetPreviousFixtureQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetPreviousFixtureQuery, FixtureResponse>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<FixtureResponse>> Handle(GetPreviousFixtureQuery request, CancellationToken cancellationToken)
    {
        var fixture = await _dbContext.Fixtures
            .AsNoTracking()
            .Where(fixture => fixture.Status == Status.Completed && fixture.KickoffTimeUtc.Value <= request.TodayStart)
            .OrderByDescending(fixture => fixture.KickoffTimeUtc.Value)
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
                    fixture.Score,
                    fixture.Outcome))
            .FirstOrDefaultAsync(cancellationToken);

        return fixture is not null
            ? fixture
            : Result.Failure<FixtureResponse>(FixtureErrors.NoneFound);
    }
}
