using FluentValidation;
using Microsoft.FluentUI.AspNetCore.Components;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Authorization.Constants;
using SportsStatistics.Web.Abstractions.Messaging;
using SportsStatistics.Web.Services;

namespace SportsStatistics.Web;

internal static class DependencyInjection
{
    public static IHostApplicationBuilder AddPresentation(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.Services.AddRazorComponents()
                        .AddInteractiveServerComponents();

        builder.Services.AddCascadingAuthenticationState();
        //builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectService>();
        //builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddHttpClient();
        builder.Services.AddFluentUIComponents();

        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultScheme = IdentityConstants.ApplicationScheme;
        //    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        //}).AddIdentityCookies();

        builder.Services
            .AddAuthorizationBuilder()
            .AddPolicy(Policies.Administrator, policy => policy.RequireRole([Roles.Administrator]))
            .AddPolicy(Policies.MatchTracker, policy => policy.RequireRole([Roles.Administrator, Roles.MatchTracker]))
            .AddPolicy(Policies.ReportsViewer, policy => policy.RequireRole([Roles.Administrator, Roles.ReportsViewer]));

        builder.Services.AddValidatorsFromAssembly(AssemblyReference.Assembly, ServiceLifetime.Singleton, includeInternalTypes: true);

        builder.Services.AddScoped<IMessenger, Messenger>();
        builder.Services.AddScoped<IClubNameService, ClubNameService>();

        return builder;
    }
}
