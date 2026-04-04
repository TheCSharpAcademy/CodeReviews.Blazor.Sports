using SportsStatistics.Application.Abstractions.Messaging;

namespace SportsStatistics.Application.Clubs.GetClub;

public sealed record GetClubQuery : IQuery<ClubResponse>;
