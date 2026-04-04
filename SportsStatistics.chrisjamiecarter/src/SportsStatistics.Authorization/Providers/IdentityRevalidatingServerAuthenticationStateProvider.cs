using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SportsStatistics.Authorization.Entities;

namespace SportsStatistics.Authorization.Providers;

/// <summary>
/// This is a server-side AuthenticationStateProvider that revalidates the security stamp for the connected user
/// every 30 minutes an interactive circuit is connected.
/// </summary>
internal sealed class IdentityRevalidatingServerAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
{
    private const int RevalidationIntervalInMinutes = 30;
    private readonly IServiceScopeFactory _scopeFactory;

    public IdentityRevalidatingServerAuthenticationStateProvider(ILoggerFactory loggerFactory, IServiceScopeFactory scopeFactory)
        : base(loggerFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(RevalidationIntervalInMinutes);

    protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        var userClaimsPrincipal = authenticationState.User;

        if (!userClaimsPrincipal.Identity?.IsAuthenticated ?? true)
        {
            return false;
        }

        // Get the services from a new scope to ensure it fetches fresh data.
        await using var scope = _scopeFactory.CreateAsyncScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

        var user = await userManager.GetUserAsync(userClaimsPrincipal);
        if (user == null)
        {
            return false;
        }

        return await signInManager.ValidateSecurityStampAsync(userClaimsPrincipal) != null;
    }
}
