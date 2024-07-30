using System.ComponentModel.DataAnnotations;


namespace SportStats.Models;



public class Player
{

    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Surname { get; set; } = string.Empty;
    [Required]
    public DateTime? BirthDate { get; set; }
    public List<BaseStat> Stats { get; set; } = [];

    public Team? MemberOf { get; set; }


}





