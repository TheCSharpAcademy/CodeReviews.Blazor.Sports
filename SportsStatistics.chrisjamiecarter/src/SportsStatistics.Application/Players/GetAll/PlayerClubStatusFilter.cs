using System.ComponentModel;

namespace SportsStatistics.Application.Players.GetAll;

public enum PlayerClubStatusFilter
{
    All,

    [Description("At club")]
    AtClub,

    [Description("Left club")]
    LeftClub
}
