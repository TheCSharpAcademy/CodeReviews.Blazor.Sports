using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SportStats.Models;

namespace SportStats.Data;

public class Seeder
{
    public StatsContext _context;

    public Seeder(StatsContext context)
    {
        _context = context;
    }

    public async Task SeedDb()
    {
        await SeedTeams();
        await SeedPlayers();
        await SeedGames();
    }

    public async Task SeedTeams()
    {
        if (!_context.Teams.Any())
        {
            var teamA = new Team() { Name = "South Dragons" };
            var teamB = new Team() { Name = "Gold Coast Blaze" };
            await _context.Teams.AddAsync(teamA);
            await _context.Teams.AddAsync(teamB);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SeedPlayers()
    {
        List<Player> players = new();
        if (!_context.Players.Any())
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t=> t.Name== "South Dragons");
            var player1 = new Player()
            {
                Name = "Shane",
                Surname = "Heal",
                BirthDate = DateTime.Now,
                MemberOf=team
            };
            players.Add(player1);
            var player2 = new Player()
            {
                Name = "Bakari",
                Surname = "Hendrix",
                BirthDate = DateTime.Now,
                MemberOf = team
            };
            players.Add(player2);
            var player3 = new Player()
            {
                Name = "Brent",
                Surname = "Hobba",
                BirthDate = DateTime.Now,
                MemberOf = team
            };
            players.Add(player3);
            var player4 = new Player()
            {
                Name = "Jakob",
                Surname = "Holmes",
                BirthDate = DateTime.Now,
                MemberOf = team
            };
            players.Add(player4);
            var player5 = new Player()
            {
                Name = "Nick",
                Surname = "Horvath",
                BirthDate = DateTime.Now,
                MemberOf = team
            };
            players.Add(player5);
        }
       foreach(var player in players) 
        {
             _context.Players.Add(player);
        }
        await _context.SaveChangesAsync();
    }

    public async Task SeedGames()
    {
        if (!_context.Games.Any())
        {
            var statsFeeder = new List<BaseStat>();
            var rnd = new Random();
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Name == "South Dragons");
            for (int j = 0; j < 10; j++)
            {
                var game = new Game()
                {
                    DateTime = DateTime.Now - TimeSpan.FromDays(rnd.Next(100)),
                    PlayedAgainst = "Flying Falcons",
                    OwnTeam = team,
                    StatsInGame=new()
                    
                };

                 _context.Games.Add(game);
                 _context.SaveChanges();

                

                foreach (var player in _context.Players)
                {

                    for (int i = 0; i < rnd.Next(1, 20); i++)
                    {
                        var stat = new Pass()
                        {
                            BelongsTo = player,
                            InGame = game,
                            Location = "",
                            Stat = Enums.StatType.Pass,
                            
                        };
                        statsFeeder.Add(stat);
                    }
                    for (int i = 0; i < rnd.Next(1, 20); i++)
                    {
                        var stat = new Shot()
                        {
                            BelongsTo = player,
                            InGame = game,
                            Location = "",
                            Stat = Enums.StatType.Shot,
                        };
                        statsFeeder.Add(stat);
                    }
                    for (int i = 0; i < rnd.Next(1, 20); i++)
                    {
                        var stat = new Interception()
                        {
                            BelongsTo = player,
                            InGame = game,
                            Location = "",
                            Stat = Enums.StatType.Interception,
                        };
                        statsFeeder.Add(stat);
                    }
                    for (int i = 0; i < rnd.Next(1, 20); i++)
                    {
                        var stat = new Block()
                        {
                            BelongsTo = player,
                            InGame = game,
                            Location = "",
                            Stat = Enums.StatType.Block,
                        };
                        statsFeeder.Add(stat);
                    }
                    for (int i = 0; i < rnd.Next(1, 20); i++)
                    {
                        var stat = new Rebound()
                        {
                            BelongsTo = player,
                            InGame = game,
                            Location = "",
                            Stat = Enums.StatType.Rebound,
                        };
                        statsFeeder.Add(stat);
                    }
                    player.Stats.AddRange(statsFeeder);
                    game.StatsInGame.AddRange(statsFeeder);
                    _context.Players.Update(player);
                    statsFeeder.Clear();

                }
                _context.Games.Update(game);
                 _context.SaveChanges();  
                 var lastGame= await _context.Games.OrderByDescending(p => p.DateTime).FirstOrDefaultAsync();
                Console.Write(lastGame);
                statsFeeder.Clear();
            }
        }
    }

      
    
}
