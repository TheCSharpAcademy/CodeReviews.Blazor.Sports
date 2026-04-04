using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Clubs;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Clubs.Update;

internal sealed class UpdateClubCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateClubCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(UpdateClubCommand request, CancellationToken cancellationToken)
    {
        var club = await _dbContext.Clubs
            .Where(club => club.Id == request.ClubId)
            .SingleOrDefaultAsync(cancellationToken);

        if (club is null)
        {
            return Result.Failure(ClubErrors.NotFound(request.ClubId));
        }

        var nameResult = Name.Create(request.Name);
        var firstFailureOrSuccess = Result.FirstFailureOrSuccess(nameResult);
        if (firstFailureOrSuccess.IsFailure)
        {
            return firstFailureOrSuccess;
        }

        club.ChangeName(nameResult.Value);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
