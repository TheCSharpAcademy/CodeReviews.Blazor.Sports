using SportsStatistics.Web.Api.Endpoints.Identity;

namespace SportsStatistics.Web.Api.Endpoints;

internal static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder builder, bool isDevelopment)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.MapIdentityEndpoints(isDevelopment);

        return builder;
    }
}
