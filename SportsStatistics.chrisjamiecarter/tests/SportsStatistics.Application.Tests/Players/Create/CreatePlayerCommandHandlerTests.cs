using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Players.Create;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Players.Create;

public class CreatePlayerCommandHandlerTests
{
    private static readonly List<Player> BasePlayers = PlayerBuilder.GetDefaults();

    private static readonly CreatePlayerCommand BaseCommand = new("New Player Name",
                                                                  2,
                                                                  "Test Nationality",
                                                                  DateOnly.FromDateTime(DateTime.Today).AddYears(-15),
                                                                  Position.Defender.Value);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly CreatePlayerCommandHandler _handler;

    public CreatePlayerCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Players)
                      .Returns(BasePlayers.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new CreatePlayerCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPlayerIsCreated()
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
    public async Task Handle_ShouldReturnFailure_WhenSquadNumberIsUnavailable()
    {
        // Arrange.
        var command = BaseCommand with { SquadNumber = BasePlayers.Last().SquadNumber };
        var expected = Result.Failure(PlayerErrors.SquadNumberTaken(command.SquadNumber));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
