using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Clubs.Update;

public sealed record UpdateClubCommand(
    Guid ClubId,
    string Name)
    : ICommand;
