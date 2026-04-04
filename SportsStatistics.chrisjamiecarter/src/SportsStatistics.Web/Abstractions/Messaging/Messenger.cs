using MediatR;
using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Web.Abstractions.Messaging;

internal sealed class Messenger(ISender sender) : IMessenger
{
    private static Error UnexpectedError => Error.Failure(
        "Messenger.Unexpected",
        "An unexpected error occured.");

    private readonly ISender _sender = sender;

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        try
        {
            return await _sender.Send(request, cancellationToken);

        }
        catch (Exception)
        {
            // IRequest<Result>.
            if (typeof(Result).IsAssignableTo(typeof(TResponse)))
            {
                return (TResponse)(object)Result.Failure(UnexpectedError);
            }

            // IRequest<Result<TResponse>>.
            if (typeof(Result<>).IsAssignableTo(typeof(TResponse)))
            {
                var innerType = typeof(TResponse).GetGenericArguments()[0];
                var failureMethod = typeof(Result).GetMethod(nameof(Result.Failure), [typeof(Error)])!.MakeGenericMethod();

                return (TResponse)failureMethod.Invoke(null, [UnexpectedError])!;
            }

            // Fallback.
            throw;
        }
    }
}
