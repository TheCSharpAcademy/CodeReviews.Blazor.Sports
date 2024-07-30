using SportStats.Enums;
using System.ComponentModel.DataAnnotations;

namespace SportStats.Models;

public class Shot : BaseStat
{
    public Shot()
    {
        Stat = StatType.Shot;
    }
}
