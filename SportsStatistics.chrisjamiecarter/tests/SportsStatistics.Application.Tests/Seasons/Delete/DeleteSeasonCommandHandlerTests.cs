using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Seasons.Delete;
using SportsStatistics.Application.Tests.Competitions;
using SportsStatistics.Application.Tests.Fixtures;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Seasons.Delete;

public class DeleteSeasonCommandHandlerTests
{
    private static readonly List<Season> BaseSeasons = SeasonBuilder.GetDefaults();
    private static readonly List<Competition> BaseCompetitions = CompetitionBuilder.GetDefaults();
    private static readonly List<Fixture> BaseFixtures = FixtureBuilder.GetDefaults();

    private static readonly DeleteSeasonCommand BaseCommand = new(BaseSeasons.First().Id);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly DeleteSeasonCommandHandler _handler;

    public DeleteSeasonCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(BaseSeasons.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(BaseCompetitions.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(BaseFixtures.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new DeleteSeasonCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSeasonIsDeleted()
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
