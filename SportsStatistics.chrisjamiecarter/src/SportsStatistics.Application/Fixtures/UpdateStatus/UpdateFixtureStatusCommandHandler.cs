using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.UpdateStatus;

internal sealed class UpdateFixtureStatusCommandHandler(
    IApplicationDbContext dbContext)
    : ICommandHandler<UpdateFixtureStatusCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(
        UpdateFixtureStatusCommand request,
        CancellationToken cancellationToken)
    {
        var fixture = await _dbContext.Fixtures
            .Where(fixture => fixture.Id == request.FixtureId)
            .SingleOrDefaultAsync(cancellationToken);

        if (fixture is null)
        {
            return Result.Failure(FixtureErrors.NotFound(request.FixtureId));
        }

        var result = fixture.ChangeStatus(request.FixtureStatus);
        if (result.IsFailure)
        {
            return result;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
