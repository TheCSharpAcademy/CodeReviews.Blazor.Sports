using SportsStatistics.Tools.DatabaseSeeder.Configuration;
using SportsStatistics.Tools.DatabaseSeeder.Services;

namespace SportsStatistics.Tools.DatabaseSeeder;

internal static class DependencyInjection
{
    public static IHostApplicationBuilder AddPresentation(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.Services.AddHostedService<Worker>();

        builder.Services.AddOpenTelemetry()
                        .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

        builder.Services
            .AddOptions<SeederOptions>()
            .BindConfiguration(nameof(SeederOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        builder.Services.AddScoped<IClubSeederService, ClubSeederService>();
        builder.Services.AddScoped<ICompetitionSeederService, CompetitionSeederService>();
        builder.Services.AddScoped<IDataSeederService, DataSeederService>();
        builder.Services.AddScoped<IFixtureSeederService, FixtureSeederService>();
        builder.Services.AddScoped<IMatchSimultationSeederService, MatchSimultationSeederService>();
        builder.Services.AddScoped<IPlayerSeederService, PlayerSeederService>();
        builder.Services.AddScoped<IRoleSeederService, RoleSeederService>();
        builder.Services.AddScoped<ISeasonSeederService, SeasonSeederService>();
        builder.Services.AddScoped<IUserSeederService, UserSeederService>();

        return builder;
    }
}
