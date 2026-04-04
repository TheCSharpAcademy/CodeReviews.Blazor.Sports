using SportsStatistics.Application.Tests.Seasons;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Tests.Competitions;

public class CompetitionBuilder : IBuildable<Competition>
{
    private Season _season = new SeasonBuilder().Build();
    private string _name = "Test Competition";
    private Format _format = Format.League;

    public CompetitionBuilder WithSeason(Season season)
    {
        _season = season;
        return this;
    }

    public CompetitionBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CompetitionBuilder WithFormat(Format format)
    {
        _format = format;
        return this;
    }

    private static Competition Create(Season season, string name, Format format) =>
        Competition.Create(
            season,
            Name.Create(name).Value,
            format);

    public Competition Build() =>
        Create(_season, _name, _format);

    public static List<Competition> GetDefaults()
    {
        var builder = new CompetitionBuilder();

        return
        [
            builder.WithName("Football League").WithFormat(Format.League).Build(),
            builder.WithName("Football Cup").WithFormat(Format.Cup).Build(),
        ];
    }
}
