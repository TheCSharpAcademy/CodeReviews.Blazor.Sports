using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Update;

internal sealed class UpdateCompetitionCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateCompetitionCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(UpdateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var competition = await _dbContext.Competitions.Where(competition => competition.Id == request.CompetitionId)
                                                       .SingleOrDefaultAsync(cancellationToken);

        if (competition is null)
        {
            return Result.Failure(CompetitionErrors.NotFound(request.CompetitionId));
        }

        var nameResult = Name.Create(request.Name);
        var formatResult = Format.Resolve(request.FormatId);
        var firstFailureOrSuccess = Result.FirstFailureOrSuccess(nameResult, formatResult);
        if (firstFailureOrSuccess.IsFailure)
        {
            return firstFailureOrSuccess;
        }

        competition.ChangeName(nameResult.Value);
        
        competition.ChangeFormat(formatResult.Value);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
