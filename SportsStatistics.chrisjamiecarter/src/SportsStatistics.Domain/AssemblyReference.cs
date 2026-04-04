using System.Reflection;

namespace SportsStatistics.Domain;

internal static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
