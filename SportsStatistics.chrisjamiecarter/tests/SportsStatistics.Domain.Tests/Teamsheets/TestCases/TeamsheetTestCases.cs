using SportsStatistics.Domain.Tests.Teamsheets.TestData;

namespace SportsStatistics.Domain.Tests.Teamsheets.TestCases;

public class CreateTeamsheetTestCase : TheoryData<Guid, DateTime>
{
    public CreateTeamsheetTestCase()
    {
        Add(TeamsheetTestData.ValidFixtureId, TeamsheetTestData.ValidSubmittedAtUtc);
    }
}

public class DeleteTeamsheetTestCase : TheoryData<DateTime>
{
    public DeleteTeamsheetTestCase()
    {
        Add(TeamsheetTestData.ValidUtcNow);
    }
}

public class AddStarterTestCase : TheoryData<Guid, Guid>
{
    public AddStarterTestCase()
    {
        Add(TeamsheetTestData.ValidFixtureId, TeamsheetTestData.ValidPlayerId);
    }
}

public class AddSubstituteTestCase : TheoryData<Guid, Guid>
{
    public AddSubstituteTestCase()
    {
        Add(TeamsheetTestData.ValidFixtureId, TeamsheetTestData.ValidPlayerId);
    }
}