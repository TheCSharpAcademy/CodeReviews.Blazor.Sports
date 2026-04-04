using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SportsStatistics.Authorization.Services;
using SportsStatistics.Web.Contracts.Requests;
using SportsStatistics.Web.Contracts.Responses;

namespace SportsStatistics.Web.Api.Endpoints.Identity;

internal static class SignoutEndpoint
{
    public const string Name = "IdentitySignout";

    public static IEndpointRouteBuilder MapSignoutEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost(Routes.Identity.Signout,
            async (ClaimsPrincipal user,
                   IAuthenticationService authenticationService,
                   [FromForm] SignoutRequest request,
                   CancellationToken cancellationToken) =>
            {
                await authenticationService.SignOutAsync().ConfigureAwait(false);

                var response = new SignoutResponse(true, "User signed out successfully");

                return Results.Ok(response);
            })
            .WithName(Name)
            .Produces<SignoutResponse>(StatusCodes.Status200OK)
            .RequireAuthorization();

        return builder;
    }
}
