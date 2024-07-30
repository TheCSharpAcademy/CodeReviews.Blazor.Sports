using SportStats.Enums;

namespace SportStats.Models;

public interface IStat
{
    public int Id { get; set; }
    public StatType Stat { get; set; }
    public Game InGame { get; set; }
    public Player BelongsTo { get; set; }
    public string Location { get; set; }

}
