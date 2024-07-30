using SportStats.Models;

namespace SportStats.Repositories;

public interface IGameRepository
{
    Task<Game> AddGame(Game game);
    Task<List<Game>> GetGamesForPlayer(Player player);
    Task UpdateGame(Game game);
}
