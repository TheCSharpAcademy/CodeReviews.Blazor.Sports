using System.Reflection;

namespace SportsStatistics.Authorization.Constants;

public static class Roles
{
    public const string Administrator = nameof(Administrator);
    public const string MatchTracker = nameof(MatchTracker);
    public const string ReportsViewer = nameof(ReportsViewer);

    public static IEnumerable<string> GetRoleNames()
    {
        return typeof(Roles)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .Select(f => (string)f.GetRawConstantValue()!);
    }
}
