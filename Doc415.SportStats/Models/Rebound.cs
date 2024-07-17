using SportStats.Enums;
using System.ComponentModel.DataAnnotations;

namespace SportStats.Models
{
    public class Rebound : BaseStat
    {
        public Rebound()
        {
            Stat = StatType.Rebound;
        }
    }
}
