using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStatistics.Application.Abstractions.Behaviours;

namespace SportsStatistics.Application;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApplication(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.Services.AddMessaging();
        builder.Services.AddValidators();

        return builder;
    }

    private static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(AssemblyReference.Assembly);

            configuration.AddOpenBehavior(typeof(RequestLoggingBehaviour<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehaviour<,>));
        });

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

        return services;
    }
}
