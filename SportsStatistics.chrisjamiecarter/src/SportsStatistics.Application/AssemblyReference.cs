using System.Reflection;

namespace SportsStatistics.Application;

internal static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
