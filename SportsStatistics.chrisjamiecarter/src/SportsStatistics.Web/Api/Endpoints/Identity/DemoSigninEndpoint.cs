using FluentValidation;
using SportsStatistics.Authorization.Services;
using SportsStatistics.Web.Api.Mappings;
using SportsStatistics.Web.Contracts.Requests;
using SportsStatistics.Web.Contracts.Responses;

namespace SportsStatistics.Web.Api.Endpoints.Identity;

internal static class DemoSigninEndpoint
{
    public const string Name = "DemoSignin";

    public static IEndpointRouteBuilder MapDemoSigninEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost(Routes.Identity.Demo.Signin,
            async (DemoSigninRequest request,
                   IAuthenticationService authenticationService,
                   IValidator<DemoSigninRequest> validator,
                   CancellationToken cancellationToken) =>
            {
                await validator.ValidateAndThrowAsync(request, cancellationToken);

                var result = await authenticationService.SignInAsync(request.Email!);

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
