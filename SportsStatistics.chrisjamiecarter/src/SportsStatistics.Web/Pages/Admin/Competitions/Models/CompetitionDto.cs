using SportsStatistics.Domain.Competitions;

namespace SportsStatistics.Web.Pages.Admin.Competitions.Models;

public sealed record CompetitionDto(
    Guid Id,
    Guid SeasonId,
    string Name,
    Format Format);
