using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.UpdateStatus;
using SportsStatistics.Application.Tests.Fixtures;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.UpdateStatus;

public class UpdateFixtureStatusCommandHandlerTests
{
    private static readonly List<Fixture> BaseFixtures = FixtureBuilder.GetDefaults();
    private static readonly Fixture BaseFixture = BaseFixtures.First();

    private static readonly UpdateFixtureStatusCommand BaseCommand = new(BaseFixture.Id, Status.InProgress);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly UpdateFixtureStatusCommandHandler _handler;

    public UpdateFixtureStatusCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(BaseFixtures.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new UpdateFixtureStatusCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenStatusIsUpdated()
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
    public async Task Handle_ShouldReturnFailure_WhenStatusIsInvalid()
    {
        // Arrange.
        var fixtureWithCompletedStatus = new FixtureBuilder().WithStatus(Status.Completed).Build();
        var fixtures = new List<Fixture> { fixtureWithCompletedStatus };
        
        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(fixtures.BuildMockDbSet().Object);

        var command = new UpdateFixtureStatusCommand(fixtureWithCompletedStatus.Id, Status.Scheduled);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.IsFailure.ShouldBeTrue();
    }
}