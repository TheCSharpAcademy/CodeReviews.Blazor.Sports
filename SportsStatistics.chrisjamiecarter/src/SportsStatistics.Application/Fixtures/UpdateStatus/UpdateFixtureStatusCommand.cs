using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures.UpdateStatus;

public sealed record UpdateFixtureStatusCommand(
    Guid FixtureId,
    Status FixtureStatus)
    : ICommand;
