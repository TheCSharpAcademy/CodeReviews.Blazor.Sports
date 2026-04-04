using MediatR;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Abstractions.Messaging;

/// <summary>
/// Defines a handler for a command request that returns a <see cref="Result"> response.
/// </summary>
/// <typeparam name="TCommand">The command type.</typeparam>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

/// <summary>
/// Defines a handler for a command request that returns a <see cref="Result"> with a response type.
/// </summary>
/// <typeparam name="TCommand">The command type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
