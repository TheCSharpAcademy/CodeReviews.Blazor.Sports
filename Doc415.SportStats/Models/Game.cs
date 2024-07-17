namespace SportStats.Models;

public class Game
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    public string PlayedAgainst { get; set; }
    public Team OwnTeam { get; set; }
    public string? GameNotes { get; set; }
    public List<BaseStat> StatsInGame { get; set; }

}
