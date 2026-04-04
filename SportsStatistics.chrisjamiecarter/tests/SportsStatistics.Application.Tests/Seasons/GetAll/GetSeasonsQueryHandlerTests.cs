using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Seasons.GetAll;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.GetAll;

public class GetSeasonsQueryHandlerTests
{
    private static readonly List<Season> BaseSeasons = SeasonBuilder.GetDefaults();

    private static readonly GetSeasonsQuery BaseCommand = new();

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetSeasonsQueryHandler _handler;

    public GetSeasonsQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();
        _handler = new GetSeasonsQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = BaseCommand;
        var seasons = BaseSeasons.ToList();
        var expected = Result.Success(seasons.ToResponse());

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(seasons.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenOne()
    {
        // Arrange.
        var command = BaseCommand;
        var seasons = BaseSeasons.Take(1).ToList();
        var expected = Result.Success(seasons.ToResponse());

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(seasons.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenNone()
    {
        // Arrange.
        var command = BaseCommand;
        var seasons = BaseSeasons.Take(0).ToList();
        var expected = Result.Success(seasons.ToResponse());

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(seasons.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
