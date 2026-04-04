using Bogus;
using SportsStatistics.Domain.MatchTracking;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Tools.DatabaseSeeder.TestData;

internal sealed class SubstitutionEventFaker
    : Faker<SubstitutionEvent>
{
    public SubstitutionEventFaker(
        Guid fixtureId,
        DateTime substitutionAtUtc,
        DateTime firstHalfStartedAtUtc,
        DateTime secondHalfStartedAtUtc,
        MatchPeriod matchPeriod,
        List<Player> playersOnField,
        List<Player> playersOnBench)
    {
        CustomInstantiator(faker =>
        {
            var playerOff = faker.Random.ListItem(playersOnField);

            var playerOn = faker.Random.ListItem(playersOnBench
                .Where(bench => bench.Position == playerOff.Position)
                .ToList());

            playersOnField.Remove(playerOff);
            playersOnBench.Remove(playerOn);

            return SubstitutionEvent.Create(
                fixtureId,
                Unwrap(Substitution.Create(playerOff.Id, playerOn.Id)),
                MatchMinuteCalculator.Calculate(substitutionAtUtc, firstHalfStartedAtUtc, secondHalfStartedAtUtc, matchPeriod),
                substitutionAtUtc);
        });
    }

    private static T Unwrap<T>(Result<T> result) =>
        result.IsSuccess
            ? result.Value
            : throw new InvalidOperationException($"Faker produced invalid data: {result.Error}");
}
