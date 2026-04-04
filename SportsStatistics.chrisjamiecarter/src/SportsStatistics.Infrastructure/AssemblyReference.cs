using System.Reflection;

namespace SportsStatistics.Infrastructure;

internal static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
