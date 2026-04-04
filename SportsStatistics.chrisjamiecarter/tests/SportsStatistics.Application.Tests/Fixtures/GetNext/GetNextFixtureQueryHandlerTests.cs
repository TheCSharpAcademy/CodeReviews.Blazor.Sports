using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.GetNext;
using SportsStatistics.Application.Tests.Competitions;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.GetNext;

public class GetNextFixtureQueryHandlerTests
{
    private readonly List<Competition> _competitions;
    private readonly List<Fixture> _fixtures;

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetNextFixtureQueryHandler _handler;

    public GetNextFixtureQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();
        
        _competitions = CompetitionBuilder.GetDefaults();
        
        // Create fixtures that match the query criteria
        // Status = Scheduled and KickoffTimeUtc >= TodayEnd
        var todayEnd = DateTime.UtcNow.AddDays(30);
        _fixtures =
        [
            new FixtureBuilder()
                .WithCompetition(_competitions.First())
                .WithStatus(Status.Scheduled)
                .WithKickoffTimeUtc(todayEnd.AddDays(1))
                .Build(),
        ];

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(_fixtures.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(_competitions.BuildMockDbSet().Object);

        _handler = new GetNextFixtureQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnNextScheduledFixture_WhenOneExists()
    {
        // Arrange.
        var query = new GetNextFixtureQuery(DateTime.UtcNow.AddDays(30));

        // Act.
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert.
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNoScheduledFixtures()
    {
        // Arrange.
        var query = new GetNextFixtureQuery(DateTime.UtcNow.AddDays(30));
        
        // Create empty fixtures list (or fixtures that don't meet criteria)
        var emptyFixtures = new List<Fixture>();
        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(emptyFixtures.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert.
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(FixtureErrors.NoneFound);
    }
}