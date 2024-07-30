using SportStats.Models;

namespace SportStats.Repositories;

public interface ITeamRepository
{
    Task AddTeam(Team team);
    Task DeleteTeam(int id);
    Task<Team> GetTeamById(int id);
    Task<List<Team>> GetTeams();
    Task UpdateTeam(Team team);

}
