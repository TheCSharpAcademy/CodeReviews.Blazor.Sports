using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Create;

internal sealed class CreatePlayerCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreatePlayerCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
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

        var squadNumberTaken = await _dbContext.Players.AsNoTracking()
                                                       .Where(existingPlayer => existingPlayer.SquadNumber == squadNumberResult.Value)
                                                       .AnyAsync(cancellationToken);

        if (squadNumberTaken)
        {
            return Result.Failure(PlayerErrors.SquadNumberTaken(request.SquadNumber));
        }

        var player = Player.Create(nameResult.Value,
                                   squadNumberResult.Value,
                                   nationalityResult.Value,
                                   dateOfBirthResult.Value,
                                   positionResult.Value);

        _dbContext.Players.Add(player);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
