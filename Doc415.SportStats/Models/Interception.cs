using SportStats.Enums;
using System.ComponentModel.DataAnnotations;

namespace SportStats.Models;

public class Interception : BaseStat
{

    public Interception()
    {
        Stat = StatType.Interception;
    }
}
