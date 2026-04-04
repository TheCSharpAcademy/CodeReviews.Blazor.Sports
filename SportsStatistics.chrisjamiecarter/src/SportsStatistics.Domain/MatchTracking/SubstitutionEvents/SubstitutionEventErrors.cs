using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.SubstitutionEvents;

public static class SubstitutionEventErrors
{
    public static Error FixtureIdIsRequired => Error.Validation(
        "SubstitutionEvent.FixtureIdIsRequired",
        "The fixture identifier is required.");

    public static Error MinuteIsRequired => Error.Validation(
        "SubstitutionEvent.MinuteIsRequired",
        "The fixture identifier is required.");

    public static Error OccurredAtDateAndTimeIsRequired => Error.Validation(
        "SubstitutionEvent.OccurredAtDateAndTimeIsRequired",
        "The occured at date and time of the substitution event is required.");

    public static Error PlayerOffIdIsRequired => Error.Validation(
        "SubstitutionEvent.PlayerOffIdIsRequired",
        "The identifier of the player being substituted off is required.");
    
    public static Error PlayerOnIdIsRequired => Error.Validation(
        "SubstitutionEvent.PlayerOnIdIsRequired",
        "The identifier of the player being substituted on is required.");

    public static class Substitution
    {
        public static Error PlayerOffIdNullOrEmpty => Error.Validation(
            "SubstitutionEvent.Substitution.PlayerOffIdNullOrEmpty",
            "The substitution event substition player off identifier cannot be null or empty.");
        
        public static Error PlayerOnIdNullOrEmpty => Error.Validation(
            "SubstitutionEvent.Substitution.Substitution",
            "The substitution event substition player on identifier cannot be null or empty.");

        public static Error SamePlayer => Error.Validation(
            "SubstitutionEvent.Substitution.SamePlayer",
            "The player being substituted on cannot be the same as the player being substituted off.");

    }
}
