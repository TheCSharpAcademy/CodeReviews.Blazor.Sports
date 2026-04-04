using Bogus;
using CountryData.Bogus;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Tools.DatabaseSeeder.TestData;

internal sealed class PlayerFaker
    : Faker<Player>
{
    public PlayerFaker(
        Queue<int> squadNumbers,
        Queue<Position> positions)
    {
        CustomInstantiator(faker =>
        {
            return Player.Create(
                Unwrap(Name.Create(faker.Name.FullName(Bogus.DataSets.Name.Gender.Male))),
                Unwrap(SquadNumber.Create(squadNumbers.Dequeue())),
                Unwrap(Nationality.Create(faker.Country().Name())),
                Unwrap(DateOfBirth.Create(DateOnly.FromDateTime(faker.Date.Past(20, DateTime.UtcNow.AddYears(-DateOfBirth.MinAge))))),
                positions.Dequeue());
        });
    }

    private static T Unwrap<T>(Result<T> result) =>
        result.IsSuccess
            ? result.Value
            : throw new InvalidOperationException($"Faker produced invalid data: {result.Error}");
}
