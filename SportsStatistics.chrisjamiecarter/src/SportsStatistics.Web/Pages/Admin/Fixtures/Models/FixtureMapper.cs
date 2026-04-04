using SportsStatistics.Application.Fixtures.Create;
using SportsStatistics.Application.Fixtures.Delete;
using SportsStatistics.Application.Fixtures.GetBySeasonId;
using SportsStatistics.Application.Fixtures.Update;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Web.Pages.Admin.Fixtures.Models;

internal static class FixtureMapper
{
    private static DateTime GetKickoffTimeUtc(this FixtureFormModel fixture)
        => fixture.KickoffDateLocal.GetValueOrDefault().ToUniversalTime().Date + fixture.KickoffTimeLocal.GetValueOrDefault().ToUniversalTime().TimeOfDay;

    private static int GetLocationValue(this FixtureFormModel fixture)
        => fixture.Location?.Value ?? -1;

    public static CreateFixtureCommand ToCreateCommand(this FixtureFormModel fixture)
        => new(fixture.Competition?.Id ?? default,
               fixture.Opponent,
               fixture.GetKickoffTimeUtc(),
               fixture.GetLocationValue());

    public static DeleteFixtureCommand ToDeleteCommand(this FixtureDto fixture)
        => new(fixture.Id);

    public static FixtureDto ToDto(this FixtureResponse fixture)
        => new(fixture.Id,
               fixture.CompetitionId,
               fixture.CompetitionName,
               fixture.Opponent,
               fixture.KickoffTimeUtc,
               fixture.LocationId,
               fixture.Location,
               fixture.HomeGoals,
               fixture.AwayGoals,
               fixture.StatusId,
               fixture.Status);

    public static LocationOptionDto ToDto(this Location location)
        => new(location.Value, location.Name);

    public static FixtureFormModel ToFormModel(this FixtureDto? fixture, IEnumerable<CompetitionDto> competitions, IEnumerable<LocationOptionDto> locations, SeasonDto season)
    {
        return fixture is null
            ? new()
            {
                Season = season,
            }
            : new()
            {
                Season = season,
                Competition = competitions.SingleOrDefault(c => c.Id == fixture.CompetitionId),
                Opponent = fixture.Opponent,
                KickoffDateLocal = fixture.KickoffTimeUtc.ToLocalTime(),
                KickoffTimeLocal = fixture.KickoffTimeUtc.ToLocalTime(),
                Location = locations.SingleOrDefault(option => option.Value == fixture.LocationId),
            };
    }

    public static IQueryable<FixtureDto> ToQueryable(this List<FixtureResponse> fixtures)
        => fixtures.Select(ToDto).AsQueryable();

    public static UpdateFixtureCommand ToUpdateCommand(this FixtureFormModel fixture, Guid id)
        => new(id,
               fixture.Opponent,
               fixture.GetKickoffTimeUtc(),
               fixture.GetLocationValue());
}
