using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.GetPrevious;
using SportsStatistics.Application.Tests.Competitions;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.GetPrevious;

public class GetPreviousFixtureQueryHandlerTests
{
    private readonly List<Competition> _competitions;
    private readonly List<Fixture> _fixtures;

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetPreviousFixtureQueryHandler _handler;

    public GetPreviousFixtureQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();
        
        _competitions = CompetitionBuilder.GetDefaults();
        
        // Create fixtures that match the query criteria
        // Status = Completed and KickoffTimeUtc <= TodayStart
        var todayStart = DateTime.UtcNow;
        _fixtures =
        [
            new FixtureBuilder()
                .WithCompetition(_competitions.First())
                .WithStatus(Status.Completed)
                .WithKickoffTimeUtc(todayStart.AddDays(-1))
                .Build(),
        ];

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(_fixtures.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(_competitions.BuildMockDbSet().Object);

        _handler = new GetPreviousFixtureQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPreviousCompletedFixture_WhenOneExists()
    {
        // Arrange.
        var query = new GetPreviousFixtureQuery(DateTime.UtcNow);

        // Act.
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert.
        result.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNoCompletedFixtures()
    {
        // Arrange.
        var query = new GetPreviousFixtureQuery(DateTime.UtcNow);
        
        // Create empty fixtures list
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