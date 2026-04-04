using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using SportsStatistics.Application.Seasons.Create;
using SportsStatistics.Application.Seasons.Delete;
using SportsStatistics.Application.Seasons.Update;
using GetAllSeasonResponse = SportsStatistics.Application.Seasons.GetAll.SeasonResponse;
using GetByIdSeasonResponse = SportsStatistics.Application.Seasons.GetById.SeasonResponse;

namespace SportsStatistics.Web.Pages.Admin.Seasons.Models;

internal static class SeasonMapper
{
    public static CreateSeasonCommand ToCreateCommand(this SeasonFormModel season)
        => new(DateOnly.FromDateTime(season.StartDate.GetValueOrDefault()),
               DateOnly.FromDateTime(season.EndDate.GetValueOrDefault()));

    public static DeleteSeasonCommand ToDeleteCommand(this SeasonDto season)
        => new(season.Id);

    public static SeasonDto ToDto(this GetAllSeasonResponse season)
        => new(season.Id,
               season.StartDate,
               season.EndDate,
               season.Name);

    public static SeasonDto ToDto(this GetByIdSeasonResponse season)
        => new(season.Id,
               season.StartDate,
               season.EndDate,
               season.Name);

    public static SeasonFormModel ToFormModel(this SeasonDto season)
        => new()
        {
            StartDate = season.StartDate.ToDateTime(),
            EndDate = season.EndDate.ToDateTime(),
        };

    public static IQueryable<SeasonDto> ToQueryable(this List<GetAllSeasonResponse> seasons)
        => seasons.Select(ToDto).AsQueryable();

    public static IQueryable<SeasonDto> ToQueryable(this List<GetByIdSeasonResponse> seasons)
        => seasons.Select(ToDto).AsQueryable();

    public static UpdateSeasonCommand ToUpdateCommand(this SeasonFormModel season, Guid id)
        => new(id,
               season.StartDate.ToDateOnly(),
               season.EndDate.ToDateOnly());

    public static DateOnly ToDateOnly(this DateTime? dateTime)
        => DateOnly.FromDateTime(dateTime.GetValueOrDefault());
}
