using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.GetCompletedBySeasonId;
using SportsStatistics.Application.Tests.Competitions;
using SportsStatistics.Application.Tests.Fixtures;
using SportsStatistics.Application.Tests.Seasons;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;

namespace SportsStatistics.Application.Tests.Fixtures.GetCompletedBySeasonId;

public class GetCompletedFixturesBySeasonIdQueryHandlerTests
{
    private readonly List<Season> _seasons;
    private readonly List<Competition> _competitions;
    private readonly List<Fixture> _fixtures;

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetCompletedFixturesBySeasonIdQueryHandler _handler;

    public GetCompletedFixturesBySeasonIdQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();
        
        _seasons = SeasonBuilder.GetDefaults();
        _competitions = 
        [
            new CompetitionBuilder().WithSeason(_seasons.First()).Build()
        ];
        
        // Create a completed fixture for the competition
        _fixtures =
        [
            new FixtureBuilder()
                .WithCompetition(_competitions.First())
                .WithStatus(Status.Completed)
                .Build()
        ];

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(_seasons.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(_competitions.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(_fixtures.BuildMockDbSet().Object);

        _handler = new GetCompletedFixturesBySeasonIdQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnCompletedFixtures_WhenSeasonExists()
    {
        // Arrange.
        var seasonId = _seasons.First().Id;
        var query = new GetCompletedFixturesBySeasonIdQuery(seasonId);

        // Act.
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert.
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoCompletedFixtures()
    {
        // Arrange.
        var seasonId = _seasons.First().Id;
        var query = new GetCompletedFixturesBySeasonIdQuery(seasonId);
        
        // No completed fixtures
        List<Fixture> fixturesWithNoCompleted = 
        [
            new FixtureBuilder()
                .WithCompetition(_competitions.First())
                .WithStatus(Status.Scheduled)
                .Build()
        ];
        
        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(fixturesWithNoCompleted.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert.
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEmpty();
    }
}