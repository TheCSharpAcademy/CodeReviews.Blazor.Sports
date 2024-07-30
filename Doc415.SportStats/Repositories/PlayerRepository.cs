using Microsoft.EntityFrameworkCore;
using SportStats.Data;
using SportStats.Models;

namespace SportStats.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        public StatsContext _context;
        public PlayerRepository(StatsContext context)
        {
            _context = context;
        }
        public async Task AddPlayer(Player player)
        {
            try
            {
                await _context.Players.AddAsync(player);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeletePlayer(int id)
        {
            try
            {
                var player = await _context.Players.FindAsync(id);

                _context.Remove(player!);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<Player> GetPlayerById(int id)
        {
            try
            {
                var player = await _context.Players.FindAsync(id);

                return player;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Player>> GetPlayers()
        {
            try
            {
                return await _context.Players.Include(e => e.Stats).Include(e => e.MemberOf).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task UpdatePlayer(Player player)
        {
            try
            {
                var playerToUpdate = await _context.Players.FindAsync(player.Id);

                playerToUpdate.Surname = player.Surname;
                playerToUpdate.Name = player.Name;
                playerToUpdate.BirthDate = player.BirthDate;
                playerToUpdate.MemberOf = player.MemberOf;
                _context.Update(playerToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<List<Player>> GetAvaiblePlayers()
        {
            try
            {
                return await _context.Players.Where(x => x.MemberOf == null).ToListAsync();

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
