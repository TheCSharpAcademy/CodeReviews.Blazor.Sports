using SportsStatistics.SharedKernel;

namespace SportsStatistics.Web.Models;

public static class FixtureFormErrors
{
    public static Error KickoffDateRequired => Error.Validation(
        "FixtureForm.KickoffDateRequired",
        "The kickoff date is required.");

    public static Error KickoffTimeRequired => Error.Validation(
        "FixtureForm.KickoffTimeRequired",
        "The kickoff time is required.");

    public static Error KickoffDateOutsideSeason(DateOnly startDate, DateOnly endDate) => Error.Validation(
        "FixtureForm.KickoffDateOutsideSeason",
        $"The kickoff date must be within the season '{startDate:d}' - '{endDate:d}'.");

    public static Error SeasonRequired => Error.Validation(
        "FixtureForm.SeasonRequired",
        "The season is required.");

    public static Error CompetitionRequired => Error.Validation(
        "FixtureForm.CompetitionRequired",
        "The competition is required.");

    public static Error LocationRequired => Error.Validation(
        "FixtureForm.LocationRequired",
        "The location is required.");

    public static Error OpponentRequired => Error.Validation(
        "FixtureForm.OpponentRequired",
        "The opponent is required.");

    public static Error OpponentExceedsMaxLength => Error.Validation(
        "FixtureForm.OpponentExceedsMaxLength",
        "The opponent name exceeds the maximum allowed length.");
}