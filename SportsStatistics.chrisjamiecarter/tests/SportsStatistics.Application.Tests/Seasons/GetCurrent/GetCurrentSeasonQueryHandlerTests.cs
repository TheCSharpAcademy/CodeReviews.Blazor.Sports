using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Seasons.GetCurrent;
using SportsStatistics.Application.Tests.Seasons;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.GetCurrent;

public class GetCurrentSeasonQueryHandlerTests
{
    private static readonly DateOnly Today = DateOnly.FromDateTime(DateTime.UtcNow);

    private readonly List<Season> _seasons;

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetCurrentSeasonQueryHandler _handler;

    public GetCurrentSeasonQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();
        
        // Create seasons with proper dates
        var pastSeason = Season.Create(DateRange.Create(Today.AddYears(-1), Today.AddDays(-1)).Value);
        var futureSeason = Season.Create(DateRange.Create(Today.AddDays(1), Today.AddYears(1).AddDays(-1)).Value);
        var currentSeason = Season.Create(DateRange.Create(Today.AddMonths(-6), Today.AddMonths(6)).Value);
        
        _seasons = [pastSeason, futureSeason, currentSeason];

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(_seasons.BuildMockDbSet().Object);

        _handler = new GetCurrentSeasonQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnCurrentSeason_WhenOneExists()
    {
        // Arrange.
        var query = new GetCurrentSeasonQuery();

        // Act.
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert.
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNoCurrentSeason()
    {
        // Arrange.
        var query = new GetCurrentSeasonQuery();
        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(new List<Season>().BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert.
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(SeasonErrors.NoCurrentSeasonFound(Today));
    }
}