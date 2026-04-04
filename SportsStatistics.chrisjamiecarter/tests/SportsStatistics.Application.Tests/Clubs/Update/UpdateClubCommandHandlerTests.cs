using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Clubs.Update;
using SportsStatistics.Application.Tests.Clubs;
using SportsStatistics.Domain.Clubs;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Clubs.Update;

public class UpdateClubCommandHandlerTests
{
    private static readonly List<Club> BaseClubs = ClubBuilder.GetDefaults();
    private static readonly Club BaseClub = BaseClubs.First();

    private static readonly UpdateClubCommand BaseCommand = new(BaseClub.Id, "Updated Club Name");

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly UpdateClubCommandHandler _handler;

    public UpdateClubCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Clubs)
                      .Returns(BaseClubs.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new UpdateClubCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenClubIsUpdated()
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
    public async Task Handle_ShouldReturnFailure_WhenClubIsNotFound()
    {
        // Arrange.
        var command = BaseCommand with { ClubId = Guid.CreateVersion7() };
        var expected = Result.Failure(ClubErrors.NotFound(command.ClubId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNameIsInvalid()
    {
        // Arrange.
        var command = BaseCommand with { Name = string.Empty };
        var expected = Result.Failure(ClubErrors.Name.IsRequired);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ClubErrors.Name.IsRequired);
    }
}