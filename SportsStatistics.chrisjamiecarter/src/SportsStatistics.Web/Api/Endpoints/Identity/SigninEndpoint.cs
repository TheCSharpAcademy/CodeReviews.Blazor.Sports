using FluentValidation;
using SportsStatistics.Authorization.Services;
using SportsStatistics.Web.Api.Mappings;
using SportsStatistics.Web.Contracts.Requests;
using SportsStatistics.Web.Contracts.Responses;

namespace SportsStatistics.Web.Api.Endpoints.Identity;

internal static class SigninEndpoint
{
    public const string Name = "IdentitySignin";

    public static IEndpointRouteBuilder MapSigninEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost(Routes.Identity.Signin,
            async (SigninRequest request,
                   IAuthenticationService authenticationService,
                   IValidator<SigninRequest> validator,
                   CancellationToken cancellationToken) =>
            {
                await validator.ValidateAndThrowAsync(request, cancellationToken);

                // Attempt password sign-in.
                var result = await authenticationService.PasswordSignInAsync(request.Email!, request.Password!, request.IsPersistant);

                var response = result.ToResponse();
                return result.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
            })
            .WithName(Name)
            .Produces<ValidationFailureResponse>(StatusCodes.Status400BadRequest)
            .Produces<SigninResponse>(StatusCodes.Status200OK)
            .Produces<SigninResponse>(StatusCodes.Status400BadRequest);

        return builder;
    }
}
