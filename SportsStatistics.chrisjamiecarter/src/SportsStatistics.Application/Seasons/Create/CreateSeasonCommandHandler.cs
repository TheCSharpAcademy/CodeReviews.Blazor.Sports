using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons.Create;

internal sealed class CreateSeasonCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateSeasonCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
    {
        var dateRangeResult = DateRange.Create(request.StartDate, request.EndDate);

        var firstFailureOrSuccess = Result.FirstFailureOrSuccess(dateRangeResult);
        if (firstFailureOrSuccess.IsFailure)
        {
            return firstFailureOrSuccess;
        }

        var season = Season.Create(dateRangeResult.Value);

        _dbContext.Seasons.Add(season);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
