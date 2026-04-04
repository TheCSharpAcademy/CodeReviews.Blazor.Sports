namespace SportsStatistics.SharedKernel;

public static class DateOnlyExtensions
{
    public static int CalculateAge(this DateOnly birthDate, DateOnly? referenceDate = null)
    {
        var today = referenceDate ?? DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - birthDate.Year;

        if (birthDate > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
