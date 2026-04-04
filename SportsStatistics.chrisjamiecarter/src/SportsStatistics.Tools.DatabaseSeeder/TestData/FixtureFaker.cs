using Bogus;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Tools.DatabaseSeeder.TestData;

internal sealed class FixtureFaker : Faker<Fixture>
{
    public FixtureFaker(
        Competition competition,
        Queue<(string Opponent, Location Location)> opponents,
        Queue<DateTime> matchDates)
    {
        CustomInstantiator(_ =>
        {
            var (opponentName, location) = opponents.Dequeue();
            var matchDate = matchDates.Dequeue();

            return competition.CreateFixture(
                Unwrap(Opponent.Create(opponentName)),
                Unwrap(KickoffTimeUtc.Create(matchDate)),
                location);
        });
    }

    private static T Unwrap<T>(Result<T> result) =>
        result.IsSuccess
            ? result.Value
            : throw new InvalidOperationException($"Faker produced invalid data: {result.Error}");
}
