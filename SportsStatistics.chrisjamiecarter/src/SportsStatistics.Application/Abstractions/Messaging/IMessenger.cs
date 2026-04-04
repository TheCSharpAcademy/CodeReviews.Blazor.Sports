using MediatR;

namespace SportsStatistics.Application.Abstractions.Messaging;

/// <summary>
/// Defines a messenger for sending requests and retrieving responses.
/// </summary>
public interface IMessenger
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}
