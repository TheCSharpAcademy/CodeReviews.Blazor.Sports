using MediatR;
using Microsoft.Extensions.Logging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Abstractions.Behaviours;

internal sealed class RequestLoggingBehaviour<TRequest, TResponse>(ILogger<RequestLoggingBehaviour<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<RequestLoggingBehaviour<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentNullException.ThrowIfNull(next, nameof(next));

        var requestName = typeof(TRequest).Name;
        var requestId = Guid.CreateVersion7();

        using var scope = _logger.BeginScope(new Dictionary<string, object>()
        {
            ["RequestId"] = requestId,
            ["RequestType"] = requestName,
        });

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Starting request {Request}", requestName);
        }

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Request {Request} payload: {@Request}", requestName, request);
        }
        try
        {
            var response = await next(cancellationToken);

            if (response is Result result)
            {
                if (result.IsSuccess)
                {
                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("Completed request {Request}", requestName);
                    }
                }
                else
                {
                    using var errorScope = _logger.BeginScope(new Dictionary<string, object>
                    {
                        ["Error"] = result.Error,
                    });
                    _logger.LogWarning("Completed request {Request} with error", requestName);
                }
            }
            else if (IsGenericResult(response))
            {
                if (GetResultIsSuccess(response))
                {
                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("Completed request {Request}", requestName);
                    }
                }
                else
                {
                    var error = GetResultError(response);
                    using var errorScope = _logger.BeginScope(new Dictionary<string, object>
                    {
                        ["ErrorType"] = error.Type,
                        ["ErrorCode"] = error.Code,
                        ["ErrorDescription"] = error.Description,
                    });
                    _logger.LogWarning("Completed request {Request} with error", requestName);
                }
            }
            else
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Completed request {Request}", requestName);
                }
            }

            return response;
        }
        catch (Exception exception)
        {
            using var exceptionScope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["ErrorType"] = "Exception",
                ["ExceptionType"] = exception.GetType().Name,
                ["ExceptionMessage"] = exception.Message,
                ["StackTrace"] = exception.StackTrace ?? string.Empty,
            });

            _logger.LogError("Exception during request {Request}", requestName);
            throw;
        }
    }

    private static bool IsGenericResult(TResponse response)
    {
        return response is not null && typeof(Result<>).IsAssignableTo(response.GetType());
    }

    private static bool GetResultIsSuccess(TResponse response)
    {
        var property = typeof(TResponse).GetProperty("IsSuccess");
        return property is not null
            && property.GetValue(response) is bool isSuccess
            && isSuccess;
    }

    private static Error GetResultError(TResponse response)
    {
        var property = typeof(TResponse).GetProperty("Error");
        return property?.GetValue(response) is Error error
            ? error
            : Error.None;
    }
}
