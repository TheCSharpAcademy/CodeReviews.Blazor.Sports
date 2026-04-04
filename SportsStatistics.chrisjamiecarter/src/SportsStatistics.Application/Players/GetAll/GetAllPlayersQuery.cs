using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Players.GetAll;

public sealed record GetAllPlayersQuery(PlayerClubStatusFilter? Filter = null) : IQuery<List<PlayerResponse>>;
