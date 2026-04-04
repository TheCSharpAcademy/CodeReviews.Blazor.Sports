namespace SportsStatistics.Web.Pages.Reports.Models;

public sealed record StatPair(int Value, int Max)
{
    public double Percentage => Max > 0 ? (double)Value / Max * 100.0 : 0;

}
