using SportsStatistics.Application.Clubs.GetClub;
using SportsStatistics.Domain.Clubs;

namespace SportsStatistics.Web.Pages.Admin.Clubs.Models;

public sealed record ClubDto(
    Guid Id,
    string Name)
{
    public ClubFormModel ToFormModel() => new()
    {
        Name = Name,
    };
}

internal static class ClubMapper
{
    public static ClubDto ToDto(this ClubResponse club) => new(
        club.ClubId,
        club.Name);
}
