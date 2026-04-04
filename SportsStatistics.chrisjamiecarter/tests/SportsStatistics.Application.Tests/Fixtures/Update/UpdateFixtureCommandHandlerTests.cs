using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.Update;
using SportsStatistics.Application.Tests.Competitions;
using SportsStatistics.Application.Tests.Seasons;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.Update;

public class UpdateFixtureCommandHandlerTests
{
    private readonly Competition _competition;
    private readonly Fixture _existingFixture;
    private readonly Fixture _targetFixture;
    private readonly Season _season;

    private readonly Mock<IApplicationDbContext> _dbContextMock;

    private readonly UpdateFixtureCommand _command;
    private readonly UpdateFixtureCommandHandler _handler;

    public UpdateFixtureCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _season = new SeasonBuilder().Build();
        _competition = new CompetitionBuilder().WithSeason(_season).Build();

        var builder = new FixtureBuilder().WithCompetition(_competition);
        _existingFixture = builder.WithKickoffTimeUtc(new(_season.DateRange.StartDate.AddDays(7), TimeOnly.MinValue)).Build();
        _targetFixture = builder.WithKickoffTimeUtc(new(_season.DateRange.StartDate.AddDays(14), TimeOnly.MinValue)).Build();

        _command = new(_targetFixture.Id,
                       "Updated Opponent",
                       _targetFixture.KickoffTimeUtc.Value.AddDays(1),
                       Location.Neutral.Value);


        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(new List<Competition>() { _competition }.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(new List<Fixture>() { _existingFixture, _targetFixture }.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(new List<Season>() { _season }.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new UpdateFixtureCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFixtureIsUpdated()
    {
        // Arrange.
        var command = _command;
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
        var command = _command with { FixtureId = Guid.CreateVersion7() };
        var expected = Result.Failure(FixtureErrors.NotFound(command.FixtureId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenCurrentFixtureIsAlreadyScheduled()
    {
        // Arrange.
        var command = _command with { KickoffTimeUtc = _targetFixture.KickoffTimeUtc };
        var expected = Result.Success();

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenAnotherFixtureAlreadyScheduled()
    {
        // Arrange.
        var command = _command with { KickoffTimeUtc = _existingFixture.KickoffTimeUtc };
        var expected = Result.Failure(FixtureErrors.AlreadyScheduledOnDate(DateOnly.FromDateTime(command.KickoffTimeUtc)));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
