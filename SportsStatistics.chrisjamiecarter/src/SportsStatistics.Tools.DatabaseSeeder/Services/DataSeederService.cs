using Microsoft.EntityFrameworkCore;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface IDataSeederService : ISeederService { }

internal sealed class DataSeederService(
    ApplicationDbContext dbContext,
    IPlayerSeederService playerSeederService,
    ISeasonSeederService seasonSeederService,
    ICompetitionSeederService competitionSeederService,
    IFixtureSeederService fixtureSeederService,
    IMatchSimultationSeederService matchSimultationSeederService)
    : IDataSeederService
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IPlayerSeederService _playerSeederService = playerSeederService;
    private readonly ISeasonSeederService _seasonSeederService = seasonSeederService;
    private readonly ICompetitionSeederService _competitionSeederService = competitionSeederService;
    private readonly IFixtureSeederService _fixtureSeederService = fixtureSeederService;
    private readonly IMatchSimultationSeederService _matchSimultationSeederService = matchSimultationSeederService;

    public async Task<Result> SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _dbContext.Players.AnyAsync(cancellationToken) || 
            await _dbContext.Seasons.AnyAsync(cancellationToken))
        {
            return Result.Success();
        }

        var playerResult = await _playerSeederService.SeedAsync(cancellationToken);
        if (playerResult.IsFailure)
        {
            return playerResult;
        }
        
        var seasonResult = await _seasonSeederService.SeedAsync(cancellationToken);
        if (seasonResult.IsFailure)
        {
            return seasonResult;
        }

        var competitionResult = await _competitionSeederService.SeedAsync(cancellationToken);
        if (competitionResult.IsFailure)
        {
            return competitionResult;
        }

        var fixtureResult = await _fixtureSeederService.SeedAsync(cancellationToken);
        if (fixtureResult.IsFailure)
        {
            return fixtureResult;
        }

        var matchSimultationResult = await _matchSimultationSeederService.SeedAsync(cancellationToken);
        if (matchSimultationResult.IsFailure)
        {
            return matchSimultationResult;
        }

        return Result.Success();
    }
}
