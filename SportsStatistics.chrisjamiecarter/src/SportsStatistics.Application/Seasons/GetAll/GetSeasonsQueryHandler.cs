using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons.GetAll;

internal sealed class GetSeasonsQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetSeasonsQuery, List<SeasonResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<SeasonResponse>>> Handle(GetSeasonsQuery request, CancellationToken cancellationToken)
    {
        var seasons = await _dbContext.Seasons.AsNoTracking()
                                              .OrderBy(season => season.DateRange.StartDate)
                                              .ThenBy(season => season.DateRange.EndDate)
                                              .ToListAsync(cancellationToken);
        return seasons.ToResponse();
    }
}
