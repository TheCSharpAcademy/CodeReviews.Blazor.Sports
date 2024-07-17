using SportStats.Enums;
using SportStats.Models;
using SportStats.Repositories;

namespace SportStats.Services;

public class StatService
{
    public IStatRepository _statRepository;

    public StatService(IStatRepository statRepository)
    {
        _statRepository = statRepository;
    }

    public async Task AddStat(BaseStat newStat)
    {
        await _statRepository.AddStat(newStat);
    }

    public async Task<PlayerChartModel> GetPlayerStatsInGameForChart(Player player, Game game)
    {
        PlayerChartModel playerChartModel = new() { Id = player.Id, Name = player.Name, StatList = new() };

        var stats = await _statRepository.GetPlayerStatsInGame(player, game);
        var statTypes = Enum.GetValues(typeof(StatType));


        playerChartModel.StatList = stats.GroupBy(x => x.Stat).
                                         Select(m => new StatChartModel { Type = m.Key, Count = m.Count() }).
                                         ToList();

        return playerChartModel;
    }

    public async Task<SeasonDataModel> GetPlayerSeasonData(Player player)
    {


        var allStats = await _statRepository.GetPlayerStats(player);
        var shotList = allStats.Where(x => x.Stat == StatType.Shot).ToList();
        var passList = allStats.Where(x => x.Stat == StatType.Pass).ToList();
        var blockList = allStats.Where(x => x.Stat == StatType.Block).ToList();
        var interceptionList = allStats.Where(x => x.Stat == StatType.Interception).ToList();
        var reboundList = allStats.Where(x => x.Stat == StatType.Rebound).ToList();

        var allDates = allStats.GroupBy(x => x.InGame.DateTime.Date).Select(x => x.Key).ToList();

        var shots = allDates
            .GroupJoin(
                shotList.GroupBy(x => x.InGame.DateTime.Date),
                date => date,
                dateGroup => dateGroup.Key,
                (date, dateGroup) => new ShotModel
                {
                    GameDate = date,
                    Count = dateGroup.SelectMany(g => g).Count()
                })
            .ToList();

        var passes = allDates
         .GroupJoin(
          passList.GroupBy(x => x.InGame.DateTime.Date),
          date => date,
          dateGroup => dateGroup.Key,
          (date, dateGroup) => new PassModel
        {
            GameDate = date,
            Count = dateGroup.SelectMany(g => g).Count()
        })
    .ToList();

        var interceptions = allDates
            .GroupJoin(
                interceptionList.GroupBy(x => x.InGame.DateTime.Date),
                date => date,
                dateGroup => dateGroup.Key,
                (date, dateGroup) => new InterceptionModel
                {
                    GameDate = date,
                    Count = dateGroup.SelectMany(g => g).Count()
                })
            .ToList();

        var blocks = allDates
    .GroupJoin(
        blockList.GroupBy(x => x.InGame.DateTime.Date),
        date => date,
        dateGroup => dateGroup.Key,
        (date, dateGroup) => new BlockModel
        {
            GameDate = date,
            Count = dateGroup.SelectMany(g => g).Count()
        })
    .ToList();

        var rebounds = allDates
    .GroupJoin(
        reboundList.GroupBy(x => x.InGame.DateTime.Date),
        date => date,
        dateGroup => dateGroup.Key,
        (date, dateGroup) => new ReboundModel
        {
            GameDate = date,
            Count = dateGroup.SelectMany(g => g).Count()
        })
    .ToList();

        var seasonData = new SeasonDataModel()
        {
            Blocks = blocks,
            Shots = shots,
            Passes = passes,
            Interceptions = interceptions,
            Rebounds = rebounds
        };

        return seasonData;
    }
}
