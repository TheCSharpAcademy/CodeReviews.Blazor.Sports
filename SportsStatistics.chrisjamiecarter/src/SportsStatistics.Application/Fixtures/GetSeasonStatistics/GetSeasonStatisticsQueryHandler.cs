using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetSeasonStatistics;

internal sealed class GetSeasonStatisticsQueryHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetSeasonStatisticsQuery, SeasonStatisticsResponse>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<SeasonStatisticsResponse>> Handle(GetSeasonStatisticsQuery request, CancellationToken cancellationToken)
    {
        var completedFixtures = await _dbContext.Fixtures
            .AsNoTracking()
            .Where(fixture => fixture.Status == Status.Completed)
            .Join(_dbContext.Competitions
                .AsNoTracking()
                .Where(competition => competition.SeasonId == request.SeasonId),
                    fixture => fixture.CompetitionId,
                    competition => competition.Id,
                    (fixture, competition) => fixture)
            .ToListAsync(cancellationToken);

        int wins = 0;
        int draws = 0;
        int losses = 0;
        int goalsFor = 0;
        int goalsAgainst = 0;

        foreach (var fixture in completedFixtures)
        {
            wins += fixture.Outcome == Outcome.Win ? 1 : 0;
            draws += fixture.Outcome == Outcome.Draw ? 1 : 0;
            losses += fixture.Outcome == Outcome.Loss ? 1 : 0;
            goalsFor += fixture.ClubGoals;
            goalsAgainst += fixture.OpponentGoals;
        }

        return new SeasonStatisticsResponse(
            completedFixtures.Count,
            wins,
            draws,
            losses,
            goalsFor,
            goalsAgainst,
            goalsFor - goalsAgainst);
    }
}
