using FluentValidation;
using SportsStatistics.Web.Api.Mappings;

namespace SportsStatistics.Web.Api.Middlewares;

public class ValidationMappingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(exception.ToResponse());

        }
    }
}
