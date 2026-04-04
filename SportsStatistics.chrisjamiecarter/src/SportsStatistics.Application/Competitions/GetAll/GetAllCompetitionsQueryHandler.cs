using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.GetAll;

internal sealed class GetAllCompetitionsQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetAllCompetitionsQuery, List<CompetitionResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<CompetitionResponse>>> Handle(GetAllCompetitionsQuery request, CancellationToken cancellationToken)
    {
        var competitions = await _dbContext.Competitions.AsNoTracking()
                                                        .Where(competition => competition.SeasonId == request.SeasonId)
                                                        .ToListAsync(cancellationToken);

        return competitions.ToResponse();
    }
}
