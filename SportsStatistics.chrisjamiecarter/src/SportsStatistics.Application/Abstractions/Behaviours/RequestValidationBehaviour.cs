using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Abstractions.Behaviours;

internal sealed class RequestValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentNullException.ThrowIfNull(next, nameof(next));

        ValidationFailure[] validationFailures = await ValidateAsync(request, _validators, cancellationToken);

        if (validationFailures.Length > 0)
        {
            var validationError = CreateValidationError(validationFailures);

            // IRequest<Result>.
            if (typeof(Result).IsAssignableTo(typeof(TResponse)))// typeof(TResponse) == typeof(Result))
            {
                return (TResponse)(object)Result.Failure(validationError);
            }

            // IRequest<Result<TResponse>>.
            if (typeof(Result<>).IsAssignableTo(typeof(TResponse))) //typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var innerType = typeof(TResponse).GetGenericArguments()[0];
                var failureMethod = typeof(Result).GetMethod(nameof(Result.Failure), [typeof(ValidationError)])!.MakeGenericMethod();

                return (TResponse)failureMethod.Invoke(null, [validationError])!;
            }

            // Fallback.
            throw new ValidationException(validationFailures);
        }

        return await next(cancellationToken);
    }

    private static async Task<ValidationFailure[]> ValidateAsync<TCommand>(TCommand command, IEnumerable<IValidator<TCommand>> validators, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return [];
        }

        var context = new ValidationContext<TCommand>(command);

        ValidationResult[] validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        ValidationFailure[] validationFailures = [.. validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)];

        return validationFailures;
    }

    private static ValidationError CreateValidationError(ValidationFailure[] validationFailures)
       => new([.. validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage))]);
}
