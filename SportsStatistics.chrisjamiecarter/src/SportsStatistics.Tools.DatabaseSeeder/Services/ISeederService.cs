using SportsStatistics.SharedKernel;

namespace SportsStatistics.Tools.DatabaseSeeder.Services;

internal interface ISeederService
{
    Task<Result> SeedAsync(CancellationToken cancellationToken = default);
}
