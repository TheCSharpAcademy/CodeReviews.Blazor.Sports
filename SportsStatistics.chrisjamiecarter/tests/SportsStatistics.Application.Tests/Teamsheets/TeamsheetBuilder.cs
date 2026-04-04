using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Teamsheets;

namespace SportsStatistics.Application.Tests.Teamsheets;

public class TeamsheetBuilder : IBuildable<Teamsheet>
{
    private Guid _fixtureId = Guid.CreateVersion7();
    private DateTime _submittedAtUtc = DateTime.UtcNow;

    public TeamsheetBuilder WithFixtureId(Guid fixtureId)
    {
        _fixtureId = fixtureId;
        return this;
    }

    public TeamsheetBuilder WithSubmittedAtUtc(DateTime submittedAtUtc)
    {
        _submittedAtUtc = submittedAtUtc;
        return this;
    }

    public Teamsheet Build() =>
        Teamsheet.Create(_fixtureId, _submittedAtUtc);

    public static List<Teamsheet> GetDefaults()
    {
        var builder = new TeamsheetBuilder();

        return
        [
            builder.WithFixtureId(Guid.CreateVersion7()).Build(),
            builder.WithFixtureId(Guid.CreateVersion7()).Build(),
        ];
    }
}