using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Competitions.Create;
using SportsStatistics.Application.Tests.Seasons;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Competitions.Create;

public class CreateCompetitionCommandHandlerTests
{
    private static readonly List<Season> BaseSeasons = SeasonBuilder.GetDefaults();

    private static readonly List<Competition> BaseCompetitions = CompetitionBuilder.GetDefaults();

    private static readonly CreateCompetitionCommand BaseCommand = new(BaseSeasons.First().Id,
                                                                       "Premier League",
                                                                       Format.League.Value);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly CreateCompetitionCommandHandler _handler;

    public CreateCompetitionCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(BaseCompetitions.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(BaseSeasons.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new CreateCompetitionCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCompetitionIsCreated()
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
    public async Task Handle_ShouldReturnFailure_WhenSeasonIsNotfound()
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
