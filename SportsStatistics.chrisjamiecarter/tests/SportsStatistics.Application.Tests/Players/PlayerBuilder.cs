using SportsStatistics.Domain.Players;

namespace SportsStatistics.Application.Tests.Players;

public class PlayerBuilder : IBuildable<Player>
{
    private string _name = "Test Player";
    private int _squadNumber = 9;
    private string _nationality = "Test Country";
    private DateOnly _dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30));
    private Position _position = Position.Midfielder;

    public PlayerBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PlayerBuilder WithSquadNumber(int squadNumber)
    {
        _squadNumber = squadNumber;
        return this;
    }

    public PlayerBuilder WithNationality(string nationality)
    {
        _nationality = nationality;
        return this;
    }

    public PlayerBuilder WithDateOfBirth(DateOnly dateOfBirth)
    {
        _dateOfBirth = dateOfBirth;
        return this;
    }

    public PlayerBuilder WithAge(int years)
    {
        _dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-years));
        return this;
    }

    public PlayerBuilder WithPosition(Position position)
    {
        _position = position;
        return this;
    }
    private static Player Create(string name, int squadNumber, string nationality, DateOnly dateOfBirth, Position position) =>
        Player.Create(
            Name.Create(name).Value,
            SquadNumber.Create(squadNumber).Value,
            Nationality.Create(nationality).Value,
            DateOfBirth.Create(dateOfBirth).Value,
            position);


    public Player Build() =>
        Create(_name, _squadNumber, _nationality, _dateOfBirth, _position);

    public static List<Player> GetDefaults()
    {
        var builder = new PlayerBuilder();

        return
        [
            builder.WithName("Goalkeeper Name").WithSquadNumber(1).WithNationality("Goalkeeper Country").WithAge(28).WithPosition(Position.Goalkeeper).Build(),
            builder.WithName("Defender Name").WithSquadNumber(4).WithNationality("Defender Country").WithAge(24).WithPosition(Position.Defender).Build(),
            builder.WithName("Midfielder Name").WithSquadNumber(7).WithNationality("Midfielder Country").WithAge(22).WithPosition(Position.Midfielder).Build(),
            builder.WithName("Attacker Name").WithSquadNumber(10).WithNationality("Attacker Country").WithAge(30).WithPosition(Position.Attacker).Build(),
        ];
    }
}
