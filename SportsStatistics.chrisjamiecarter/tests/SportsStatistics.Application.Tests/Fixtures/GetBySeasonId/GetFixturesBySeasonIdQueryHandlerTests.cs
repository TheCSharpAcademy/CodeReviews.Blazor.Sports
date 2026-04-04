using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Fixtures.GetBySeasonId;
using SportsStatistics.Application.Tests.Competitions;
using SportsStatistics.Application.Tests.Seasons;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Fixtures.GetBySeasonId;

public class GetFixturesBySeasonIdQueryHandlerTests
{
    private readonly List<Competition> _competitions;
    private readonly List<Fixture> _fixtures;

    private readonly GetFixturesBySeasonIdQuery _command;

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetFixturesBySeasonIdQueryHandler _handler;

    public GetFixturesBySeasonIdQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        var seasons = SeasonBuilder.GetDefaults();
        _competitions = [];
        _fixtures = [];

        _command = new(seasons.First().Id);

        foreach (var season in seasons)
        {
            var competitionBuilder = new CompetitionBuilder().WithSeason(season);

            _competitions.Add(competitionBuilder.WithName("Competition League").WithFormat(Format.League).Build());
            _competitions.Add(competitionBuilder.WithName("Competition Cup").WithFormat(Format.League).Build());

            foreach (var competition in _competitions)
            {
                var fixtureBuilder = new FixtureBuilder().WithCompetition(competition);

                _fixtures.Add(fixtureBuilder.WithOpponent("Home Game").WithKickoffTimeUtc(new(season.DateRange.StartDate.AddDays(7), TimeOnly.MinValue)).WithLocation(Location.Home).Build());
                _fixtures.Add(fixtureBuilder.WithOpponent("Away Game").WithKickoffTimeUtc(new(season.DateRange.StartDate.AddDays(14), TimeOnly.MinValue)).WithLocation(Location.Away).Build());
            }
        }

        _dbContextMock.Setup(m => m.Competitions)
                      .Returns(_competitions.BuildMockDbSet().Object);

        _handler = new GetFixturesBySeasonIdQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenMany()
    {
        // Arrange.
        var command = _command;
        var fixtures = _fixtures.ToList();

        var expected = Result.Success(fixtures.Where(fixture => _competitions.Any(c => c.SeasonId == command.SeasonId && c.Id == fixture.CompetitionId)).Select(fixture => 
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
    public async Task Handle_ShouldReturnSuccess_WhenOne()
    {
        // Arrange.
        var command = _command;
        var fixtures = _fixtures.Take(1).ToList();
        var expected = Result.Success(fixtures.Where(fixture => _competitions.Any(c => c.SeasonId == command.SeasonId && c.Id == fixture.CompetitionId)).Select(fixture =>
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
        var command = _command;
        var fixtures = _fixtures.Take(0).ToList();
        var expected = Result.Success(fixtures.Where(fixture => _competitions.Any(c => c.SeasonId == command.SeasonId && c.Id == fixture.CompetitionId)).Select(fixture =>
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
