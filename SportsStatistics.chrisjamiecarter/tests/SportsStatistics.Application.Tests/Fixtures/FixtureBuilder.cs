using SportsStatistics.Application.Tests.Competitions;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using System.Reflection;

namespace SportsStatistics.Application.Tests.Fixtures;

public class FixtureBuilder : IBuildable<Fixture>
{
    private Competition _competition = new CompetitionBuilder().Build();
    private string _opponent = "Test Opponent";
    private DateTime _kickoffTimeUtc = DateTime.UtcNow;
    private Location _location = Location.Home;
    private Status _status = Status.Scheduled;

    public FixtureBuilder WithCompetition(Competition competition)
    {
        _competition = competition;
        return this;
    }

    public FixtureBuilder WithOpponent(string opponent)
    {
        _opponent = opponent;
        return this;
    }

    public FixtureBuilder WithKickoffTimeUtc(DateTime kickoffTimeUtc)
    {
        _kickoffTimeUtc = kickoffTimeUtc;
        return this;
    }

    public FixtureBuilder WithLocation(Location location)
    {
        _location = location;
        return this;
    }

    public FixtureBuilder WithStatus(Status status)
    {
        _status = status;
        return this;
    }

    public Fixture Build()
    {
        // Create base fixture
        var fixture = Fixture.Create(
            _competition,
            Opponent.Create(_opponent).Value,
            KickoffTimeUtc.Create(_kickoffTimeUtc).Value,
            _location);
        
        // Use reflection to set the Status property (it's private set but we need it for testing)
        var statusProperty = typeof(Fixture).GetProperty("Status", BindingFlags.Public | BindingFlags.Instance);
        statusProperty?.SetValue(fixture, _status);
        
        return fixture;
    }

    public static List<Fixture> GetDefaults()
    {
        var builder = new FixtureBuilder();

        return
        [
            builder.WithOpponent("League Opponent One").WithKickoffTimeUtc(DateTime.UtcNow.AddDays(7)).WithLocation(Location.Home).Build(),
            builder.WithOpponent("League Opponent Two").WithKickoffTimeUtc(DateTime.UtcNow.AddDays(14)).WithLocation(Location.Away).Build(),
        ];
    }
}
