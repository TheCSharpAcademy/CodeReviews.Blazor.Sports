using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Fixtures.GetByDate;

public static class GetFixturesByDateQueryErrors
{
    public static Error FixtureDateIsRequired => Error.Validation(
        "GetFixtureByDateQuery.FixtureDateIsRequired",
        "The date of the fixture is required.");
}
