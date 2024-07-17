using Microsoft.EntityFrameworkCore;
using SportStats.Data;
using SportStats.Models;

namespace SportStats.Repositories
{

    public class TeamRepository : ITeamRepository
    {
        public StatsContext _context;

        public TeamRepository(StatsContext context)
        {
            _context = context;
        }

        public async Task AddTeam(Team team)
        {
            try
            {
                await _context.Teams.AddAsync(team);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

        }

        public async Task DeleteTeam(int id)
        {
            try
            {
                var teamToDelete = await _context.Teams.FindAsync(id);
                var playersOfTeam = await _context.Players.Where(x => x.MemberOf == teamToDelete).ToListAsync();
                _context.Teams.Remove(teamToDelete);

                foreach (var player in playersOfTeam)
                {
                    player.MemberOf = null;
                    _context.Players.Update(player);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        public async Task<Team> GetTeamById(int id)
        {
            try
            {
                return await _context.Teams.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<List<Team>> GetTeams()
        {
            try
            {
                return await _context.Teams.Include(x => x.Players).ThenInclude(player => player.Stats).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task UpdateTeam(Team team)
        {
            var teamToUpdate = await _context.Teams.FindAsync(team.Id);
            teamToUpdate.Id = team.Id;
            teamToUpdate.Name = team.Name;
            teamToUpdate.Players = team.Players;
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }




    }
}
