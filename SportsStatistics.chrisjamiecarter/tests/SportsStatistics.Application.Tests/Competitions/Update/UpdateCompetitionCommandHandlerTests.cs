using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Competitions.Update;
using SportsStatistics.Application.Tests.Seasons;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Competitions.Update;

public class UpdateCompetitionCommandHandlerTests
{
    private static readonly List<Season> BaseSeasons = SeasonBuilder.GetDefaults();

    private static readonly List<Competition> BaseCompetitions = CompetitionBuilder.GetDefaults();

    private static readonly UpdateCompetitionCommand BaseCommand = new(BaseCompetitions.First().Id,
                                                                       "Updated Name",
                                                                       Format.Cup.Value);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly UpdateCompetitionCommandHandler _handler;

    public UpdateCompetitionCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(BaseCompetitions.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(BaseSeasons.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new UpdateCompetitionCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCompetitionIsUpdated()
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
    public async Task Handle_ShouldReturnFailure_WhenCompetitionIsNotFound()
    {
        // Arrange.
        var command = BaseCommand with { CompetitionId = Guid.CreateVersion7() };
        var expected = Result.Failure(CompetitionErrors.NotFound(command.CompetitionId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
