using SportsStatistics.Application.Abstractions.Messaging;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures.UpdateScore;

public sealed record UpdateFixtureScoreCommand(
    Guid FixtureId,
    Score FixtureScore)
    : ICommand;
