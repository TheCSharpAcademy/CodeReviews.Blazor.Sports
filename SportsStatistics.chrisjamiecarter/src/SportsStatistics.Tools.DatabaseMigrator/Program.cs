using SportsStatistics.Infrastructure;

namespace SportsStatistics.Tools.DatabaseMigrator;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.AddServiceDefaults();

        builder.AddPresentation();
        builder.AddInfrastructure();

        var host = builder.Build();

        await host.RunAsync();
    }
}
