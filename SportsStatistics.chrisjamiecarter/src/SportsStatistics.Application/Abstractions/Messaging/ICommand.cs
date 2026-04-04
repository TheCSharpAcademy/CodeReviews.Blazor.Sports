using MediatR;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Abstractions.Messaging;

/// <summary>
/// Defines a command request that returns a <see cref="Result"> response.
/// </summary>
public interface ICommand : IRequest<Result>
{
}

/// <summary>
/// Defines a command request that returns a <see cref="Result"> with a response type.
/// </summary>
/// <typeparam name="TResponse">The command response type.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
