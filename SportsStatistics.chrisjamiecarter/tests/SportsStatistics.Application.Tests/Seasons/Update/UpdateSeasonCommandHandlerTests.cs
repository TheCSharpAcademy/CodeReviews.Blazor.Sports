using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Seasons.Update;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.Update;

public class UpdateSeasonCommandHandlerTests
{
    private static readonly List<Season> BaseSeasons = SeasonBuilder.GetDefaults();

    private static readonly UpdateSeasonCommand BaseCommand = new(BaseSeasons.First().Id,
                                                                  BaseSeasons.First().DateRange.StartDate.AddDays(1),
                                                                  BaseSeasons.First().DateRange.EndDate.AddDays(-1));

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly UpdateSeasonCommandHandler _handler;

    public UpdateSeasonCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(BaseSeasons.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new UpdateSeasonCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSeasonIsUpdated()
    {
        // Arrange.
        var command = BaseCommand;
        var expected = Result.Success();

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenSeasonIsNotFound()
    {
        // Arrange.
        var command = BaseCommand with { SeasonId = Guid.CreateVersion7() };
        var expected = Result.Failure(SeasonErrors.NotFound(command.SeasonId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
