using SportStats.Enums;
using System.ComponentModel.DataAnnotations;

namespace SportStats.Models
{
    public class Block : BaseStat
    {
        public Block()
        {
            Stat = StatType.Block;
        }
    }
}
