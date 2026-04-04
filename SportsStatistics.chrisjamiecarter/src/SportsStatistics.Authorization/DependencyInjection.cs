using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStatistics.Aspire.Constants;
using SportsStatistics.Authorization.Database;
using SportsStatistics.Authorization.Entities;
using SportsStatistics.Authorization.Providers;
using SportsStatistics.Authorization.Schemas;
using SportsStatistics.Authorization.Services;

namespace SportsStatistics.Authorization;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddAuthorizationInternal(this IHostApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.AddSqlServerDbContext<IdentityDbContext>(Resources.SqlDatabase, configureDbContextOptions: options =>
        {
            options.UseSqlServer(sqlOptions =>
            {
                sqlOptions.MigrationsHistoryTable(IdentitySchema.MigrationsHistory.Table, IdentitySchema.MigrationsHistory.Schema);
            });
        });

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<IdentityDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/signin";
            options.LogoutPath = "/signout";
            options.AccessDeniedPath = "/access-denied";
        });

        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingServerAuthenticationStateProvider>();

        return builder;
    }
}
