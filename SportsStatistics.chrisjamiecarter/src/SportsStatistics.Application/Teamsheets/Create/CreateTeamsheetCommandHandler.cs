using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Teamsheets;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Teamsheets.Create;

internal sealed class CreateTeamsheetCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateTeamsheetCommand>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result> Handle(CreateTeamsheetCommand request, CancellationToken cancellationToken)
    {
        var fixture = await _dbContext.Fixtures.AsNoTracking()
                                               .Where(fixture => fixture.Id == request.FixtureId)
                                               .SingleOrDefaultAsync(cancellationToken);

        if (fixture is null)
        {
            return Result.Failure(FixtureErrors.NotFound(request.FixtureId));
        }

        var existingTeamsheet = await _dbContext.Teamsheets.AsNoTracking()
                                                           .Where(teamsheet => teamsheet.FixtureId == request.FixtureId)
                                                           .AnyAsync(cancellationToken);

        if (existingTeamsheet)
        {
            return Result.Failure(TeamsheetErrors.FixtureId.AlreadyHasTeamsheet);
        }

        var allPlayerIds = await _dbContext.Players.AsNoTracking()
                                                   .Select(p => p.Id)
                                                   .ToListAsync(cancellationToken);

        var invalidPlayerIds = request.StarterIds.Distinct()
                                                 .Where(id => !allPlayerIds.Contains(id))
                                                 .ToList();

        if (invalidPlayerIds.Count != 0)
        {
            return Result.Failure(TeamsheetErrors.StarterIds.PlayerNotFound);
        }

        var teamsheet = Teamsheet.Create(request.FixtureId, DateTime.UtcNow);

        foreach (var starterId in request.StarterIds.Distinct())
        {
            teamsheet.AddStarter(starterId);
        }

        var substituteIds = allPlayerIds.Where(id => !request.StarterIds.Contains(id))
                                        .ToList();

        foreach (var substituteId in substituteIds)
        {
            teamsheet.AddSubstitute(substituteId);
        }

        _dbContext.Teamsheets.Add(teamsheet);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
