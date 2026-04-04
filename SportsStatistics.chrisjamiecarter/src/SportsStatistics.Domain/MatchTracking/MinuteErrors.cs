using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking;

/// <summary>
/// Contains error definitions for match minute validation.
/// </summary>
public static class MinuteErrors
{
    /// <summary>
    /// Error when minute value is null.
    /// </summary>
    public static Error Null => Error.Validation(
        "MatchTracking.Minute.Null",
        "The match minute cannot be null.");

    /// <summary>
    /// Error when base minute is below the minimum allowed value (BR-007).
    /// </summary>
    public static Error BelowMinimum => Error.Validation(
        "MatchTracking.Minute.BelowMinimum",
        "The match minute cannot be zero. The earliest possible recorded value is 1.");

    /// <summary>
    /// Error when base minute exceeds the maximum allowed value.
    /// </summary>
    public static Error AboveMaximum => Error.Validation(
        "MatchTracking.Minute.AboveMaximum",
        "The match minute exceeds the maximum allowed value of 120.");

    /// <summary>
    /// Error when the minute is not valid for the first half (BR-003).
    /// </summary>
    public static Error InvalidForFirstHalf => Error.Validation(
        "MatchTracking.Minute.InvalidForFirstHalf",
        "Regulation events in the first half must be recorded in minutes 1-45.");

    /// <summary>
    /// Error when the minute is not valid for the second half (BR-004).
    /// </summary>
    public static Error InvalidForSecondHalf => Error.Validation(
        "MatchTracking.Minute.InvalidForSecondHalf",
        "Regulation events in the second half must be recorded in minutes 46-90. Minute 45 cannot be assigned to a second-half event.");

    /// <summary>
    /// Error when stoppage minute is provided for a non-stoppage period.
    /// </summary>
    public static Error StoppageMinuteNotAllowed => Error.Validation(
        "MatchTracking.Minute.StoppageMinuteNotAllowed",
        "A stoppage minute cannot be specified for regulation time. Use stoppage notation (e.g., 45+1) only during stoppage time.");

    /// <summary>
    /// Error when stoppage minute is zero or negative.
    /// </summary>
    public static Error InvalidStoppageMinute => Error.Validation(
        "MatchTracking.Minute.InvalidStoppageMinute",
        "The stoppage minute must be a positive integer (1 or greater).");

    /// <summary>
    /// Error when the minute is not valid for extra time first half (BR-006).
    /// </summary>
    public static Error InvalidForExtraTimeFirstHalf => Error.Validation(
        "MatchTracking.Minute.InvalidForExtraTimeFirstHalf",
        "Events in the first period of extra time must be recorded in minutes 91-105.");

    /// <summary>
    /// Error when the minute is not valid for extra time second half (BR-006).
    /// </summary>
    public static Error InvalidForExtraTimeSecondHalf => Error.Validation(
        "MatchTracking.Minute.InvalidForExtraTimeSecondHalf",
        "Events in the second period of extra time must be recorded in minutes 106-120.");

    /// <summary>
    /// Error when an invalid base minute is provided for stoppage time notation.
    /// </summary>
    public static Error InvalidStoppageBaseMinute => Error.Validation(
        "MatchTracking.Minute.InvalidStoppageBaseMinute",
        "Stoppage time base minute must be 45 or 90.");
}
