using SportStats.Models;

namespace SportStats.Repositories
{
    public interface IStatRepository
    {
        Task AddStat(BaseStat stat);
        Task<List<BaseStat>> GetPlayerStatsInGame(Player player, Game game);
        Task<List<BaseStat>> GetPlayerStats(Player player);
    }
}
