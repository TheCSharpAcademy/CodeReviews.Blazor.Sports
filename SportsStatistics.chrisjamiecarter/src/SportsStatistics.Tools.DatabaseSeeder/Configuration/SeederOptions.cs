namespace SportsStatistics.Tools.DatabaseSeeder.Configuration;

public sealed class SeederOptions
{
    public string DefaultPassword { get; init; } = "Password123!";
    public bool SeedTestData { get; init; } = true;
    public int YearsOfData { get; init; } = 3;
    public AdminUserOptions Admin { get; init; } = new();
    public ReportsUserOptions Reports { get; init; } = new();
    public TrackerUserOptions Tracker { get; init; } = new();
}
