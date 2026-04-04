namespace SportsStatistics.SharedKernel;

public static class EnumerationErrors
{
    public static readonly Error Unresolved = Error.NotFound(
        "Enumeration.Unresolved",
        "The enumeration value could not resolved.");
}
