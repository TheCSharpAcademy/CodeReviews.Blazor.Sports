namespace SportsStatistics.Application.Tests;

public interface IBuildable<T>
{
    static abstract List<T> GetDefaults();
    T Build();
}