using MockQueryable.Moq;
using SportsStatistics.Application.Abstractions.Data;
using SportsStatistics.Application.Clubs.GetClub;
using SportsStatistics.Application.Tests.Clubs;
using SportsStatistics.Domain.Clubs;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Tests.Clubs.GetClub;

public class GetClubQueryHandlerTests
{
    private static readonly List<Club> BaseClubs = ClubBuilder.GetDefaults();

    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly GetClubQueryHandler _handler;

    public GetClubQueryHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();

        _dbContextMock.Setup(m => m.Clubs)
                      .Returns(BaseClubs.BuildMockDbSet().Object);

        _handler = new GetClubQueryHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnClub_WhenClubExists()
    {
        // Arrange.
        var query = new GetClubQuery();
        var expected = BaseClubs.First();

        // Act.
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert.
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        result.Value.Name.ShouldBe(expected.Name.Value);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenNoClubExists()
    {
        // Arrange.
        var query = new GetClubQuery();
        _dbContextMock.Setup(m => m.Clubs)
                      .Returns(new List<Club>().BuildMockDbSet().Object);

        // Act.
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert.
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(ClubErrors.NoneFound);
    }
}