using SportStats.Models;
using SportStats.Repositories;

namespace SportStats.Services;

public class TeamService
{
    ITeamRepository _repository;

    public TeamService(ITeamRepository repository)
    {
        _repository = repository;
    }

    public async Task AddTeam(Team team)
    {
        await _repository.AddTeam(team);
    }

    public async Task DeleteTeam(int id)
    {
        await _repository.DeleteTeam(id);
    }

    public async Task<Team> GetTeamById(int id)
    {
        return await _repository.GetTeamById(id);
    }

    public async Task<List<Team>> GetTeams()
    {
        return await _repository.GetTeams();
    }

    public async Task UpdateTeam(Team team)
    {
        await _repository.UpdateTeam(team);
    }




}



