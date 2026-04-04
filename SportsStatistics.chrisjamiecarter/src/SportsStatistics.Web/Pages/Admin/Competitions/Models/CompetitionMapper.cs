using SportsStatistics.Application.Competitions.Create;
using SportsStatistics.Application.Competitions.Delete;
using SportsStatistics.Application.Competitions.GetAll;
using SportsStatistics.Application.Competitions.Update;
using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Web.Pages.Admin.Competitions.Models;

internal static class CompetitionMapper
{
    public static CreateCompetitionCommand ToCreateCommand(this CompetitionFormModel competition) => new(
        competition.Season?.Id ?? default,
        competition.Name,
        competition.Format?.Value ?? -1);

    public static DeleteCompetitionCommand ToDeleteCommand(this CompetitionDto competition) => new(competition.Id);

    public static CompetitionDto ToDto(this CompetitionResponse competition) => new(
        competition.Id,
        competition.SeasonId,
        competition.Name,
        competition.Format);

    public static FormatOptionDto ToDto(this Format format) => new(
        format.Value, 
        format.Name);

    public static CompetitionFormModel ToFormModel(this CompetitionDto? competition, SeasonDto season, IEnumerable<FormatOptionDto> formatOptions)
    {
        return competition is null
            ? new()
            {
                Season = season,
            }
            : new()
            {
                Season = season,
                Name = competition.Name,
                Format = formatOptions.SingleOrDefault(option => option.Value == competition.Format.Value),
        };
    }

    public static IQueryable<CompetitionDto> ToQueryable(this IEnumerable<CompetitionResponse> competitions)
        => competitions.Select(ToDto).AsQueryable();

    public static UpdateCompetitionCommand ToUpdateCommand(this CompetitionFormModel competition, Guid id)
        => new(id,
               competition.Name,
               competition.Format?.Value ?? -1);
}
