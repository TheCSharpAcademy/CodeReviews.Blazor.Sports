using System.Reflection;

namespace SportsStatistics.Authorization;

internal static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
