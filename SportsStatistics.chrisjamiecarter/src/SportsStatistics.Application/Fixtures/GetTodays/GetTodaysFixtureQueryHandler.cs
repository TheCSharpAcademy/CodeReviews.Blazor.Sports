using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetTodays;

internal sealed class GetTodaysFixtureQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetTodaysFixtureQuery, FixtureResponse>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<FixtureResponse>> Handle(GetTodaysFixtureQuery request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var todayStart = today.ToDateTime(TimeOnly.MinValue);
        var todayEnd = today.ToDateTime(TimeOnly.MaxValue);

        var fixture = await _dbContext.Fixtures
            .AsNoTracking()
            .Where(fixture => fixture.KickoffTimeUtc.Value >= todayStart && fixture.KickoffTimeUtc.Value <= todayEnd)
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
                    fixture.Status,
                    fixture.Score,
                    fixture.Outcome))
            .FirstOrDefaultAsync(cancellationToken);

        return fixture is not null
            ? fixture
            : Result.Failure<FixtureResponse>(FixtureErrors.NoneFound);
    }
}
