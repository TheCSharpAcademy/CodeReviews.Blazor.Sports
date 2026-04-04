using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Update;

internal sealed class UpdatePlayerCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdatePlayerCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _dbContext.Players
            .Where(player => player.Id == request.PlayerId)
            .SingleOrDefaultAsync(cancellationToken);

        if (player is null)
        {
            return Result.Failure(PlayerErrors.NotFound(request.PlayerId));
        }

        var nameResult = Name.Create(request.Name);
        var squadNumberResult = SquadNumber.Create(request.SquadNumber);
        var nationalityResult = Nationality.Create(request.Nationality);
        var dateOfBirthResult = DateOfBirth.Create(request.DateOfBirth);
        var positionResult = Position.Resolve(request.PositionId);

        var firstFailureOrSuccess = Result.FirstFailureOrSuccess(nameResult,
                                                                 squadNumberResult,
                                                                 nationalityResult,
                                                                 dateOfBirthResult,
                                                                 positionResult);
        if (firstFailureOrSuccess.IsFailure)
        {
            return firstFailureOrSuccess;
        }

        var squadNumberTaken = await _dbContext.Players
            .AsNoTracking()
            .Where(existing => existing.Id != player.Id && existing.SquadNumber == squadNumberResult.Value)
            .AnyAsync(cancellationToken);

        if (squadNumberTaken)
        {
            return Result.Failure(PlayerErrors.SquadNumberTaken(request.SquadNumber));
        }

        player.ChangeName(nameResult.Value);

        player.ChangeSquadNumber(squadNumberResult.Value);

        player.ChangeNationality(nationalityResult.Value);

        player.ChangeDateOfBirth(dateOfBirthResult.Value);

        player.ChangePosition(positionResult.Value);

        player.ChangeLeftClub(request.LeftClub);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
