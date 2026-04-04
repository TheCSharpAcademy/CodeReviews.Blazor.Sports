namespace SportsStatistics.Web.Pages.MatchTracker.Models;

public sealed record SubstitutionDialogData(
    IEnumerable<PlayerOptionDto> PlayerOptions,
    PlayerDto? PlayerOff,
    Guid? PlayerOnId,
    DateTime OccurredAtUtc);
