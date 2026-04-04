using MediatR;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Abstractions.Messaging;

/// <summary>
/// Defines a handler for a query request that returns a <see cref="Result"> response of type <see cref="TResponse".
/// </summary>
/// <typeparam name="TQuery">The command type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
