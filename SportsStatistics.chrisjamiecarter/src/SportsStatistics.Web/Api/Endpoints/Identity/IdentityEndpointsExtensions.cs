namespace SportsStatistics.Web.Api.Endpoints.Identity;

internal static class IdentityEndpointsExtensions
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder builder, bool isDevelopment)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.MapSigninEndpoint();
        builder.MapSignoutEndpoint();

        if (isDevelopment)
        {
            builder.MapDemoSigninEndpoint();
        }

        return builder;
    }
}
