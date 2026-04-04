using System.Reflection;

namespace SportsStatistics.Tools.DatabaseMigrator;

internal static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
