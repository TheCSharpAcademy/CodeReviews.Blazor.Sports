using SportsStatistics.Tools.DatabaseMigrator.Services;

namespace SportsStatistics.Tools.DatabaseMigrator;

internal static class DependencyInjection
{
    public static IHostApplicationBuilder AddPresentation(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.Services.AddHostedService<Worker>();

        builder.Services.AddOpenTelemetry()
                        .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

        builder.Services.AddScoped<IDatabaseMigratorService, DatabaseMigratorService>();

        return builder;
    }
}
