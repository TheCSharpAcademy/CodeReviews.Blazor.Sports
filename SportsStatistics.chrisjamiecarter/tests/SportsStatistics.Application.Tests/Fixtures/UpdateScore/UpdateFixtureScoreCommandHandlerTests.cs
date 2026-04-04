using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.UpdateScore;
using SportsStatistics.Application.Tests.Fixtures;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.UpdateScore;

public class UpdateFixtureScoreCommandHandlerTests
{
    private static readonly List<Fixture> BaseFixtures = 
    [
        new FixtureBuilder().WithStatus(Status.InProgress).Build()
    ];
    private static readonly Fixture BaseFixture = BaseFixtures.First();

    private static readonly UpdateFixtureScoreCommand BaseCommand = new(BaseFixture.Id, Score.Create(2, 1).Value);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly UpdateFixtureScoreCommandHandler _handler;

    public UpdateFixtureScoreCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(BaseFixtures.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new UpdateFixtureScoreCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenScoreIsUpdated()
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
    public async Task Handle_ShouldReturnFailure_WhenFixtureIsNotFound()
    {
        // Arrange.
        var command = BaseCommand with { FixtureId = Guid.CreateVersion7() };
        var expected = Result.Failure(FixtureErrors.NotFound(command.FixtureId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenFixtureStatusDoesNotAllowScoreChange()
    {
        // Arrange.
        var scheduledFixture = new FixtureBuilder().WithStatus(Status.Scheduled).Build();
        var fixtures = new List<Fixture> { scheduledFixture };
        
        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(fixtures.BuildMockDbSet().Object);

        var command = new UpdateFixtureScoreCommand(scheduledFixture.Id, Score.Create(2, 1).Value);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.IsFailure.ShouldBeTrue();
    }
}