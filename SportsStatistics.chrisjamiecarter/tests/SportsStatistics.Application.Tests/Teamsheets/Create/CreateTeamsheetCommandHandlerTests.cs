using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Teamsheets.Create;
using SportsStatistics.Application.Tests.Fixtures;
using SportsStatistics.Application.Tests.Players;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Teamsheets;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Teamsheets.Create;

public class CreateTeamsheetCommandHandlerTests
{
    private static readonly List<Fixture> BaseFixtures = FixtureBuilder.GetDefaults();
    private static readonly List<Player> BasePlayers = PlayerBuilder.GetDefaults();

    private readonly Fixture _fixture;

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly CreateTeamsheetCommandHandler _handler;

    public CreateTeamsheetCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _fixture = BaseFixtures.First();

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(BaseFixtures.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Players)
                      .Returns(BasePlayers.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Teamsheets)
                      .Returns(new List<Teamsheet>().BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new CreateTeamsheetCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenTeamsheetIsCreated()
    {
        // Arrange.
        var starterIds = BasePlayers.Take(Teamsheet.RequiredNumberOfStarters).Select(p => p.Id).ToList();
        var command = new CreateTeamsheetCommand(_fixture.Id, starterIds);
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
        var starterIds = BasePlayers.Take(Teamsheet.RequiredNumberOfStarters).Select(p => p.Id).ToList();
        var command = new CreateTeamsheetCommand(Guid.CreateVersion7(), starterIds);
        var expected = Result.Failure(FixtureErrors.NotFound(command.FixtureId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenTeamsheetAlreadyExists()
    {
        // Arrange.
        var existingFixture = BaseFixtures.First();
        var starterIds = BasePlayers.Take(Teamsheet.RequiredNumberOfStarters).Select(p => p.Id).ToList();
        
        // Create a teamsheet for this fixture first
        var existingTeamsheet = Teamsheet.Create(existingFixture.Id, DateTime.UtcNow);
        
        var teamsheetsWithFixture = new List<Teamsheet> { existingTeamsheet };
        
        _dbContextMock.Setup(m => m.Teamsheets)
                      .Returns(teamsheetsWithFixture.BuildMockDbSet().Object);
        
        var command = new CreateTeamsheetCommand(existingFixture.Id, starterIds);
        var expected = Result.Failure(TeamsheetErrors.FixtureId.AlreadyHasTeamsheet);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenStarterPlayerNotFound()
    {
        // Arrange.
        var starterIds = new List<Guid> { Guid.CreateVersion7() };
        var command = new CreateTeamsheetCommand(_fixture.Id, starterIds);
        var expected = Result.Failure(TeamsheetErrors.StarterIds.PlayerNotFound);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}