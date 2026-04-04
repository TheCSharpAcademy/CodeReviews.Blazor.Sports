using SportsStatistics.Domain.Clubs;

namespace SportsStatistics.Application.Tests.Clubs;

public class ClubBuilder : IBuildable<Club>
{
    private Name _name = Name.Create("Test Club").Value;

    public ClubBuilder WithName(Name name)
    {
        _name = name;
        return this;
    }

    public ClubBuilder WithName(string name)
    {
        _name = Name.Create(name).Value;
        return this;
    }

    public Club Build() =>
        Club.Create(_name);

    public static List<Club> GetDefaults()
    {
        var builder = new ClubBuilder();

        return
        [
            builder.WithName("Premier League Club").Build(),
        ];
    }
}