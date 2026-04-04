namespace SportsStatistics.SharedKernel;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateTime birthDate, DateTime? referenceDate = null)
    {
        var today = referenceDate ?? DateTime.Today;
        var age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
