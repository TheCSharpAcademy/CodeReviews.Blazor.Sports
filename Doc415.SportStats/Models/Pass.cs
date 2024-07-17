using SportStats.Enums;
using System.ComponentModel.DataAnnotations;

namespace SportStats.Models
{
    public class Pass : BaseStat
    {
        public Pass() {
            Stat = StatType.Pass;
        }
    }
}
