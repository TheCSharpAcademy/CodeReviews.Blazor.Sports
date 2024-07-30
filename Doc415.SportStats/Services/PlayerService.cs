using SportStats.Models;
using SportStats.Repositories;

namespace SportStats.Services;

public class PlayerService
{
    IPlayerRepository _repository;

    public PlayerService(IPlayerRepository repository)
    {
        _repository = repository;
    }

    public async Task AddPlayer(Player player)
    {
        await _repository.AddPlayer(player);
    }

    public async Task DeletePlayer(int id)
    {
        await _repository.DeletePlayer(id);
    }

    public async Task<Player> GetPlayerById(int id)
    {
        return await _repository.GetPlayerById(id);
    }

    public async Task<List<Player>> GetPlayers()
    {
        return await _repository.GetPlayers();
    }

    public async Task UpdatePlayer(Player player)
    {
        await _repository.UpdatePlayer(player);
    }

    public async Task<List<Player>> GetAvaiblePlayers()
    {
        return await _repository.GetAvaiblePlayers();
    }
}

