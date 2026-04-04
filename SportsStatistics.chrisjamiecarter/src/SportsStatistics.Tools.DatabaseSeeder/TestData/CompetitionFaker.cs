using Bogus;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Tools.DatabaseSeeder.TestData;

internal sealed class CompetitionFaker
    : Faker<Competition>
{
    public CompetitionFaker(
        Season season,
        Queue<(string, Format)> competitions)
    {
        CustomInstantiator(faker =>
        {
            var competition = competitions.Dequeue();
            return season.CreateCompetition(
                Unwrap(Name.Create(competition.Item1)),
                competition.Item2);
        });
    }

    private static T Unwrap<T>(Result<T> result) =>
        result.IsSuccess
            ? result.Value
            : throw new InvalidOperationException($"Faker produced invalid data: {result.Error}");
}
