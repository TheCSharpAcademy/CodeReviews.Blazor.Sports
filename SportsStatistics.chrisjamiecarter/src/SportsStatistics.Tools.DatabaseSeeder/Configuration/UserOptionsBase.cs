namespace SportsStatistics.Tools.DatabaseSeeder.Configuration;

public abstract class UserOptionsBase(
    string username,
    string email,
    string role)
{
    public string Username { get; init; } = username;
    public string Email { get; init; } = email;
    public string Role { get; init; } = role;
}
