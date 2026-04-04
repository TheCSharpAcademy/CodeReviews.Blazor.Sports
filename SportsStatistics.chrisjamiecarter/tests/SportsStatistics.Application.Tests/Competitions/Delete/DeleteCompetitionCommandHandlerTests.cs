using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Competitions.Delete;
using SportsStatistics.Application.Tests.Fixtures;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Competitions.Delete;

public class DeleteCompetitionCommandHandlerTests
{
    private static readonly List<Competition> BaseCompetitions = CompetitionBuilder.GetDefaults();
    private static readonly List<Fixture> BaseFixtures = FixtureBuilder.GetDefaults();

    private static readonly DeleteCompetitionCommand BaseCommand = new(BaseCompetitions.First().Id);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly DeleteCompetitionCommandHandler _handler;

    public DeleteCompetitionCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(BaseCompetitions.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(BaseFixtures.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new DeleteCompetitionCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCompetitionIsDeleted()
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
