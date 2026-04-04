using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons.GetCurrent;

internal sealed class GetCurrentSeasonQueryHandler(
    IApplicationDbContext dbContext) 
    : IQueryHandler<GetCurrentSeasonQuery, SeasonResponse>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<SeasonResponse>> Handle(GetCurrentSeasonQuery request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var response = await _dbContext.Seasons
            .AsNoTracking()
            .Where(season => season.DateRange.StartDate <= today && season.DateRange.EndDate >= today)
            .OrderByDescending(season => season.DateRange.StartDate)
            .Select(season => new SeasonResponse(season.Id, season.DateRange.StartDate, season.DateRange.EndDate, season.Name))
            .FirstOrDefaultAsync(cancellationToken);

        return response is not null
            ? response
            : Result.Failure<SeasonResponse>(SeasonErrors.NoCurrentSeasonFound(today));
    }
}
