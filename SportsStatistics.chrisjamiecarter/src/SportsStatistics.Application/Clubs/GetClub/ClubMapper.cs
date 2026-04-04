using SportsStatistics.Domain.Clubs;

namespace SportsStatistics.Application.Clubs.GetClub;

internal static class ClubMapper
{
    public static ClubResponse ToResponse(this Club club) => new(
            club.Id,
            club.Name);
}