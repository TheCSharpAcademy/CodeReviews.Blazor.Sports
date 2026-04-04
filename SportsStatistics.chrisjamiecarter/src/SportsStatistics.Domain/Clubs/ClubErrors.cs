using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Clubs;

public static class ClubErrors
{
    public static Error NoneFound => Error.NotFound(
        "Club.NoneFound",
        "No club was found.");

    public static Error NotFound(Guid id) => Error.NotFound(
        "Club.NotFound",
        $"The club with the identifier '{id}' was not found.");

    public static class Id
    {
        public static Error IsRequired => Error.Validation(
            "Club.Id.IsRequired",
            "The club identifier is required.");
    }

    public static class Name
    {
        public static Error ExceedsMaxLength => Error.Validation(
            "Club.Name.ExceedsMaxLength",
            $"The club name must be {Clubs.Name.MaxLength} characters or less.");

        public static Error IsRequired => Error.Validation(
            "Club.Name.IsRequired",
            "The club name is required.");
    }
}
