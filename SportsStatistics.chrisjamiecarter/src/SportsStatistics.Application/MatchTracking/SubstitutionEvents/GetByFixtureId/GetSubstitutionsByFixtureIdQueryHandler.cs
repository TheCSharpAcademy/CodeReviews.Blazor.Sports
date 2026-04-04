using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.MatchTracking.SubstitutionEvents.GetByFixtureId;

internal sealed class GetSubstitutionsByFixtureIdQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetSubstitutionsByFixtureIdQuery, List<SubstitutionResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<List<SubstitutionResponse>>> Handle(GetSubstitutionsByFixtureIdQuery request, CancellationToken cancellationToken)
    {
        var substitutions = await _dbContext.SubstitutionEvents.AsNoTracking()
                                                               .Where(e => e.FixtureId == request.FixtureId)
                                                               .OrderBy(e => e.Minute.BaseMinute)
                                                               .ThenBy(e => e.Minute.StoppageMinute ?? 0)
                                                               .ThenBy(e => e.OccurredAtUtc)
                                                               .ToListAsync(cancellationToken);

        return substitutions.ToResponse();
    }
}
