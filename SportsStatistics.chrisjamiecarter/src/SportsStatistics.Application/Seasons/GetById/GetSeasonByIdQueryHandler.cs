using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons.GetById;

internal sealed class GetSeasonByIdQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetSeasonByIdQuery, SeasonResponse>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<SeasonResponse>> Handle(GetSeasonByIdQuery request, CancellationToken cancellationToken)
    {
        var season = await _dbContext.Seasons.AsNoTracking()
                                             .Where(season => season.Id == request.SeasonId)
                                             .SingleOrDefaultAsync(cancellationToken);

        return season is not null
            ? season.ToResponse()
            : Result.Failure<SeasonResponse>(SeasonErrors.NotFound(request.SeasonId));
    }
}
