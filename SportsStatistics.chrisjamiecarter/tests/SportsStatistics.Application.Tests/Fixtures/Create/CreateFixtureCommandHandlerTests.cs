using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.Create;
using SportsStatistics.Application.Tests.Competitions;
using SportsStatistics.Application.Tests.Seasons;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.Create;

public class CreateFixtureCommandHandlerTests
{
    private readonly Competition _competition;
    private readonly Fixture _fixture;
    private readonly Season _season;

    private readonly Mock<IApplicationDbContext> _dbContextMock;

    private readonly CreateFixtureCommand _command;
    private readonly CreateFixtureCommandHandler _handler;

    public CreateFixtureCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _season = new SeasonBuilder().Build();
        _competition = new CompetitionBuilder().WithSeason(_season).Build();
        _fixture = new FixtureBuilder().WithCompetition(_competition).Build();

        _command = new(_competition.Id,
                       "Test Opponent",
                       new(_season.DateRange.StartDate.AddDays(7), TimeOnly.MinValue),
                       Location.Away.Value);

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(new List<Competition>() { _competition }.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(new List<Fixture>() { _fixture }.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.Seasons)
                      .Returns(new List<Season>() { _season }.BuildMockDbSet().Object);

        _dbContextMock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        _handler = new CreateFixtureCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFixtureIsCreated()
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
    public async Task Handle_ShouldReturnFailure_WhenCompetitionIsNotFound()
    {
        // Arrange.
        var command = _command with { CompetitionId = Guid.CreateVersion7() };
        var expected = Result.Failure(CompetitionErrors.NotFound(command.CompetitionId));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenKickoffTimeOutsideSeason()
    {
        // Arrange.
        var command = _command with { KickoffTimeUtc = new(_season.DateRange.StartDate.AddDays(-1), TimeOnly.MinValue) };
        var expected = Result.Failure(FixtureErrors.KickoffTimeOutsideSeason(command.KickoffTimeUtc, _season.DateRange.StartDate, _season.DateRange.EndDate));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenAnotherFixtureAlreadyScheduled()
    {
        // Arrange.
        var command = _command with { KickoffTimeUtc = _fixture.KickoffTimeUtc };
        var expected = Result.Failure(FixtureErrors.AlreadyScheduledOnDate(DateOnly.FromDateTime(command.KickoffTimeUtc)));

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
