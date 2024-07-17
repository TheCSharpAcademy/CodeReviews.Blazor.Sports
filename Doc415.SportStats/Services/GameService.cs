using SportStats.Models;
using SportStats.Repositories;
using SQLitePCL;
using System.Reflection.Metadata.Ecma335;

namespace SportStats.Services;

public class GameService
{
    IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Game> AddGame(Game game)
    { 
        return await _gameRepository.AddGame(game); 
    }

    public async Task<List<Game>> GetGamesForPlayer(Player player)
    {
        return await _gameRepository.GetGamesForPlayer(player);
    }

    public async Task UpdateGame(Game game)
    {
        await _gameRepository.UpdateGame(game);
    }
}
