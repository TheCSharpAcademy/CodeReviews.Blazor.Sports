using Bogus;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Teamsheets;

namespace SportsStatistics.Tools.DatabaseSeeder.TestData;

internal sealed class TeamsheetFaker
    : Faker<Teamsheet>
{
    public TeamsheetFaker(Fixture fixture, List<Player> players)
    {
        CustomInstantiator(faker =>
        {
            // Classic 4-4-2 formation.
            var starters = new List<Player>();
            
            var goalkeepers = players.Where(player => player.Position == Position.Goalkeeper);
            starters.AddRange(faker.Random.Shuffle(goalkeepers).Take(1));
            
            var defenders = players.Where(player => player.Position == Position.Defender);
            starters.AddRange(faker.Random.Shuffle(defenders).Take(4));
            
            var midfielders = players.Where(player => player.Position == Position.Midfielder);
            starters.AddRange(faker.Random.Shuffle(midfielders).Take(4));

            var attackers = players.Where(player => player.Position == Position.Attacker);
            starters.AddRange(faker.Random.Shuffle(attackers).Take(2));

            var teamsheet = Teamsheet.Create(fixture.Id, DateTime.UtcNow);
            foreach (var player in players)
            {
                if (starters.Contains(player))
                {
                    teamsheet.AddStarter(player.Id);
                }
                else
                {
                    teamsheet.AddSubstitute(player.Id);
                }
            }

            return teamsheet;
        });
    }
}
