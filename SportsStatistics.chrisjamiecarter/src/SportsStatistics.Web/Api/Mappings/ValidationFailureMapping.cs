using FluentValidation;
using FluentValidation.Results;
using SportsStatistics.Web.Contracts.Responses;

namespace SportsStatistics.Web.Api.Mappings;

internal static class ValidationFailureMapping
{
    public static ValidationResponse ToResponse(this ValidationFailure failure)
        => new(failure.PropertyName, failure.ErrorMessage);

    public static ValidationFailureResponse ToResponse(this ValidationException exception)
        => new(exception.Errors.Select(ToResponse));
}
