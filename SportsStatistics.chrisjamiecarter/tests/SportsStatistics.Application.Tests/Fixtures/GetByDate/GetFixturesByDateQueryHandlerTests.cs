using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.GetByDate;
using SportsStatistics.Application.Tests.Competitions;
using SportsStatistics.Application.Tests.Seasons;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.GetByDate;

public class GetFixturesByDateQueryHandlerTests
{
    private readonly List<Competition> _competitions;

    private readonly GetFixturesByDateQuery _command = new(DateOnly.MinValue);

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetFixturesByDateQueryHandler _handler;

    public GetFixturesByDateQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _competitions = [];

        var startDate = new DateOnly(DateTime.UtcNow.Year, 1, 1);
        var endDate = startDate.AddYears(1);
        var season = new SeasonBuilder().WithStartDate(startDate).WithEndDate(endDate).Build();

        var competition = new CompetitionBuilder().WithSeason(season).Build();
        _competitions.Add(competition);

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(_competitions.BuildMockDbSet().Object);

        _handler = new GetFixturesByDateQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenOne()
    {
        // Arrange.
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var command = _command with { FixtureDate = today } ;
        var fixtures = new List<Fixture>();
        var fixtureBuilder = new FixtureBuilder().WithCompetition(_competitions.First());
        fixtures.Add(fixtureBuilder.WithKickoffTimeUtc(new(today.AddDays(-1), TimeOnly.MinValue)).Build());
        fixtures.Add(fixtureBuilder.WithKickoffTimeUtc(new(today, TimeOnly.MinValue)).Build());
        fixtures.Add(fixtureBuilder.WithKickoffTimeUtc(new(today.AddDays(1), TimeOnly.MinValue)).Build());
        var expected = Result.Success(fixtures.Where(fixture => DateOnly.FromDateTime(fixture.KickoffTimeUtc.Value) == today).Select(fixture =>
        {
            var competition = _competitions.First(c => c.Id == fixture.CompetitionId);
            return fixture.ToResponse(competition);
        }).ToList());

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(fixtures.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenNone()
    {
        // Arrange.
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var command = _command with { FixtureDate = today };
        var fixtures = new List<Fixture>();
        var fixtureBuilder = new FixtureBuilder().WithCompetition(_competitions.First());
        fixtures.Add(fixtureBuilder.WithKickoffTimeUtc(new(today.AddDays(-1), TimeOnly.MinValue)).Build());
        fixtures.Add(fixtureBuilder.WithKickoffTimeUtc(new(today.AddDays(1), TimeOnly.MinValue)).Build());

        var expected = Result.Success(fixtures.Where(fixture => DateOnly.FromDateTime(fixture.KickoffTimeUtc.Value) == today).Select(fixture =>
        {
            var competition = _competitions.First(c => c.Id == fixture.CompetitionId);
            return fixture.ToResponse(competition);
        }).ToList());

        _dbContextMock.Setup(m => m.Fixtures)
                      .Returns(fixtures.BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert.
        result.ShouldBeEquivalentTo(expected);
    }
}
