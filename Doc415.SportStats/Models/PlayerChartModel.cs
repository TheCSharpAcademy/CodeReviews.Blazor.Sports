using SportStats.Enums;

namespace SportStats.Models;

public class StatChartModel
{
    public StatType Type { get; set; }
    public int Count { get; set; }
    public DateTime? Date { get; set; }

}

public class PlayerChartModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<StatChartModel> StatList { get; set; }
}

public class GameStatsModel
{
    public StatType StatType { get; set; }
   
    public List<StatDataModel> StatDataModels { get; set; }
}

public class StatDataModel
{
    public DateTime GameDate { get; set; }

    public int Count { get; set; }
}

public class SeasonDataModel
{
    public List <ShotModel> Shots {  get; set; }
    public List<PassModel> Passes { get; set; }
    public List<BlockModel> Blocks { get; set; }
    public List<InterceptionModel> Interceptions { get; set; }
    public List<ReboundModel> Rebounds { get; set; }
}

public class ShotModel
{
    public DateTime GameDate { get; set; }
    public int Count { get; set; }
}

public class PassModel
{
    public DateTime GameDate { get; set; }
    public int Count { get; set; }
}

public class BlockModel
{
    public DateTime GameDate { get; set; }
    public int Count { get; set; }
}

public class InterceptionModel
{
    public DateTime GameDate { get; set; }
    public int Count { get; set; }
}

public class ReboundModel
{
    public DateTime GameDate { get; set; }
    public int Count { get; set; }
}

