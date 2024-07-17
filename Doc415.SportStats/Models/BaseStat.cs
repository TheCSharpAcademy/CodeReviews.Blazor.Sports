using SportStats.Enums;
using System.ComponentModel.DataAnnotations;

namespace SportStats.Models
{
    public abstract class BaseStat : IStat
    {
        public int Id { get; set; }
        [Required]
        public Game InGame { get; set; }
        [Required]
        public Player BelongsTo { get; set; }
        [Required]
        public StatType Stat { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
