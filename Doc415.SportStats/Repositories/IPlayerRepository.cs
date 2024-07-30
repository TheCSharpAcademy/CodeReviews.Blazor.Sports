using SportStats.Models;

namespace SportStats.Repositories;

public interface IPlayerRepository
{
    Task<List<Player>> GetPlayers();
    Task<Player> GetPlayerById(int id);
    Task DeletePlayer(int id);
    Task UpdatePlayer(Player player);
    Task AddPlayer(Player player);
    Task<List<Player>> GetAvaiblePlayers();
}
