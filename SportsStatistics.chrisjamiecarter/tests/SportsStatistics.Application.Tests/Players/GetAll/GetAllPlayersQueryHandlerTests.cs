using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Players.GetAll;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Players.GetAll;

public class GetAllPlayersQueryHandlerTests
{
    private static readonly List<Player> BasePlayers = PlayerBuilder.GetDefaults();
    
    private static readonly GetAllPlayersQuery BaseCommand = new();

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetAllPlayersQueryHandler _handler;

    public GetAllPlayersQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();
        _handler = new GetAllPlayersQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = BaseCommand;
        var players = BasePlayers;
        var expected = Result.Success(players.ToResponse().ToList());

        _dbContextMock.Setup(m => m.Players)
                      .Returns(players.BuildMockDbSet().Object);

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
        var players = BasePlayers.Take(1).ToList();
        var expected = Result.Success(players.ToResponse().ToList());

        _dbContextMock.Setup(m => m.Players)
                      .Returns(players.BuildMockDbSet().Object);

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
        var players = BasePlayers.Take(0).ToList();
        var expected = Result.Success(players.ToResponse().ToList());

        _dbContextMock.Setup(m => m.Players)
                      .Returns(players.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
