using Microsoft.EntityFrameworkCore;
using SportStats.Data;
using SportStats.Models;

namespace SportStats.Repositories;

public class GameRepository : IGameRepository
{
    public StatsContext _context;

    public GameRepository(StatsContext context)
    {
        _context = context;
    }

    public async Task<Game> AddGame(Game game)
    {
        try
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();

            var recordedGame = _context.Games.Include(g=> g.StatsInGame).OrderByDescending(e => e.Id).FirstOrDefault();
            return recordedGame;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<List<Game>> GetGamesForPlayer(Player player)
    {
        try
        {
            return await _context.Games.Where(x => x.OwnTeam.Players.Contains(player)).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task UpdateGame(Game game)
    {
        try
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
    }

}
