namespace SportsStatistics.Application.Models;

public record MigrationResult(bool IsSuccess,
                              string Message,
                              int MigrationsApplied = 0,
                              TimeSpan Duration = default,
                              Exception? Exception = null)
{
    private const string DefaultSuccessMessage = "Database migration completed successfully.";
    private const string DefaultUpToDateMessage = "No pending migrations to apply.";

    public static MigrationResult Success(int migrationsApplied, TimeSpan duration, string? message = null) =>
        new(true, message ?? DefaultSuccessMessage, migrationsApplied, duration);

    public static MigrationResult Failure(string message, Exception? exception = null) =>
        new(false, message, 0, default, exception);

    public static MigrationResult UpToDate(TimeSpan duration) =>
        new(true, DefaultUpToDateMessage, 0, duration);
}