using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Aspire.Constants;
using SportsStatistics.Authorization;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.DomainEvents;

namespace SportsStatistics.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructure(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        var connectionString = builder.Configuration.GetConnectionString(Resources.SqlDatabase) ?? throw new InvalidOperationException($"Unable to get connection string.");

        builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable(Schemas.MigrationsHistory.Table, Schemas.MigrationsHistory.Schema);
            });
        }, ServiceLifetime.Transient);

        builder.EnrichSqlServerDbContext<ApplicationDbContext>();

        builder.AddAuthorizationInternal();

        builder.Services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();

        return builder;
    }
}
