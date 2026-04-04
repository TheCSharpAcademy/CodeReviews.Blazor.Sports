using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Clubs;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Clubs.GetClub;

internal sealed class GetClubQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetClubQuery, ClubResponse>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<ClubResponse>> Handle(GetClubQuery request, CancellationToken cancellationToken)
    {
        var club = await _dbContext.Clubs
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        return club is null
            ? Result.Failure<ClubResponse>(ClubErrors.NoneFound)
            : Result.Success(club.ToResponse());
    }
}
