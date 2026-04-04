using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Competitions.Create;

internal sealed class CreateCompetitionCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateCompetitionCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var season = await _dbContext.Seasons.AsNoTracking()
                                             .Where(season => season.Id == request.SeasonId)
                                             .SingleOrDefaultAsync(cancellationToken);

        if (season is null)
        {
            return Result.Failure(SeasonErrors.NotFound(request.SeasonId));
        }

        var nameResult = Name.Create(request.Name);
        var formatResult = Format.Resolve(request.FormatId);

        var firstFailureOrSuccess = Result.FirstFailureOrSuccess(nameResult, formatResult);
        if (firstFailureOrSuccess.IsFailure)
        {
            return firstFailureOrSuccess;
        }

        var competition = season.CreateCompetition(nameResult.Value, formatResult.Value);

        _dbContext.Competitions.Add(competition);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
