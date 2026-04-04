using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Fixtures.Delete;

public sealed record DeleteFixtureCommand(Guid FixtureId) : ICommand;
